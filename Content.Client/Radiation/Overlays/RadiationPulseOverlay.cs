using System.Numerics;
using Content.Shared.Radiation.Components;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Enums;
using Robust.Shared.Graphics;
using Robust.Shared.Map;
using Robust.Shared.Physics;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Client.Radiation.Overlays
{
    public sealed class RadiationPulseOverlay : Overlay
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        private TransformSystem? _transform;

        private const float MaxDist = 15.0f;

        public override OverlaySpace Space => OverlaySpace.WorldSpace;
        public override bool RequestScreenTexture => true;

        private readonly ShaderInstance _baseShader;
        private readonly Dictionary<EntityUid, (ShaderInstance shd, RadiationShaderInstance instance)> _pulses = new();

        public RadiationPulseOverlay()
        {
            IoCManager.InjectDependencies(this);
            _baseShader = _prototypeManager.Index<ShaderPrototype>("Radiation").Instance().Duplicate();
        }

        protected override bool BeforeDraw(in OverlayDrawArgs args)
        {
            RadiationQuery(args.Viewport.Eye);
            return _pulses.Count > 0;
        }

        protected override void Draw(in OverlayDrawArgs args)
        {
            if (ScreenTexture == null)
                return;

            var worldHandle = args.WorldHandle;
            var viewport = args.Viewport;

            foreach ((var shd, var instance) in _pulses.Values)
            {
                if (instance.CurrentMapCoords.MapId != args.MapId)
                    continue;

                // To be clear, this needs to use "inside-viewport" pixels.
                // In other words, specifically NOT IViewportControl.WorldToScreen (which uses outer coordinates).
                var tempCoords = viewport.WorldToLocal(instance.CurrentMapCoords.Position);
                tempCoords.Y = viewport.Size.Y - tempCoords.Y;
                shd?.SetParameter("renderScale", viewport.RenderScale);
                shd?.SetParameter("positionInput", tempCoords);
                shd?.SetParameter("range", instance.Range);
                var life = CalcLife((float)(_gameTiming.RealTime - instance.Start).TotalSeconds);
                shd?.SetParameter("life", (float) life);

                // There's probably a very good reason not to do this.
                // Oh well!
                shd?.SetParameter("SCREEN_TEXTURE", viewport.RenderTarget.Texture);

                worldHandle.UseShader(shd);
                worldHandle.DrawRect(Box2.CenteredAround(instance.CurrentMapCoords.Position, new Vector2(instance.Range, instance.Range) * 2f), Color.White);
            }

            worldHandle.UseShader(null);
        }

        //every single number is taken from a ceiling. that log + sine function rises and then oscillates, 
        //so shader doesn't just freeze/develop very slowly
        private float CalcLife(float lifetime)
        {
            var value = Math.Min(0.8, Math.Log(lifetime + 1, 4)) + 0.1 * Math.Sin(lifetime);
            return (float) value;
        }

        //Queries all pulses on the map and either adds or removes them from the list of rendered pulses based on whether they should be drawn (in range? on the same z-level/map? pulse entity still exists?)
        private void RadiationQuery(IEye? currentEye)
        {
            _transform ??= _entityManager.System<TransformSystem>();

            if (currentEye == null)
            {
                _pulses.Clear();
                return;
            }

            var currentEyeLoc = currentEye.Position;

            var pulses = _entityManager.EntityQueryEnumerator<RadiationPulseComponent>();
            //Add all pulses that are not added yet but qualify
            while (pulses.MoveNext(out var pulseEntity, out var pulse))
            {
                if (!_pulses.ContainsKey(pulseEntity) && PulseQualifies(pulseEntity, currentEyeLoc))
                {
                    _pulses.Add(
                            pulseEntity,
                            (
                                _baseShader.Duplicate(),
                                new RadiationShaderInstance(
                                    _transform.GetMapCoordinates(pulseEntity),
                                    pulse.VisualRange,
                                    pulse.StartTime
                                )
                            )
                    );
                }
            }

            var activeShaderIds = _pulses.Keys;
            foreach (var pulseEntity in activeShaderIds) //Remove all pulses that are added and no longer qualify
            {
                if (_entityManager.EntityExists(pulseEntity) &&
                    PulseQualifies(pulseEntity, currentEyeLoc) &&
                    _entityManager.TryGetComponent(pulseEntity, out RadiationPulseComponent? pulse))
                {
                    var shaderInstance = _pulses[pulseEntity];
                    shaderInstance.instance.CurrentMapCoords = _transform.GetMapCoordinates(pulseEntity);
                    shaderInstance.instance.Range = pulse.VisualRange;
                } else {
                    _pulses[pulseEntity].shd.Dispose();
                    _pulses.Remove(pulseEntity);
                }
            }

        }

        private bool PulseQualifies(EntityUid pulseEntity, MapCoordinates currentEyeLoc)
        {
            var transformComponent = _entityManager.GetComponent<TransformComponent>(pulseEntity);
            var transformSystem = _entityManager.System<SharedTransformSystem>();
            return transformComponent.MapID == currentEyeLoc.MapId
                && transformComponent.Coordinates.InRange(_entityManager, transformSystem, EntityCoordinates.FromMap(transformComponent.ParentUid, currentEyeLoc, transformSystem, _entityManager), MaxDist);
        }

        private sealed record RadiationShaderInstance(MapCoordinates CurrentMapCoords, float Range, TimeSpan Start)
        {
            public MapCoordinates CurrentMapCoords = CurrentMapCoords;
            public float Range = Range;
            public TimeSpan Start = Start;
        };
    }
}

