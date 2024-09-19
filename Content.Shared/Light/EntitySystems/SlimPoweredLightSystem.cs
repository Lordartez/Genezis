using Content.Shared.Light.Components;
using Content.Shared.Power;
using Content.Shared.Power.EntitySystems;

namespace Content.Shared.Light.EntitySystems;

public sealed class SlimPoweredLightSystem : EntitySystem
{
    [Dependency] private readonly SharedPowerReceiverSystem _receiver = default!;
    [Dependency] private readonly SharedPointLightSystem _lights = default!;

    private bool _setting;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SlimPoweredLightComponent, PowerChangedEvent>(OnLightPowerChanged);
    }

    private void OnLightPowerChanged(Entity<SlimPoweredLightComponent> ent, ref PowerChangedEvent args)
    {
        // Early out if we don't need to trycomp.
        if (args.Powered)
        {
            if (!ent.Comp.Enabled)
                return;
        }
        else
        {
            if (!ent.Comp.Enabled)
                return;
        }

        if (!_lights.TryGetLight(ent.Owner, out var light))
            return;

        var enabled = ent.Comp.Enabled && args.Powered;
        _setting = true;
        _lights.SetEnabled(ent.Owner, enabled, light);
        _setting = false;
    }

    public void SetEnabled(Entity<SlimPoweredLightComponent?> entity, bool enabled)
    {
        if (!Resolve(entity.Owner, ref entity.Comp, false))
            return;

        if (entity.Comp.Enabled == enabled)
            return;

        entity.Comp.Enabled = enabled;
        Dirty(entity);
        _lights.SetEnabled(entity.Owner, enabled);
    }
}
