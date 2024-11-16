using Content.Shared.Light.Components;
using Robust.Shared.Physics.Events;
using Robust.Shared.Physics.Systems;

namespace Content.Shared.Light.EntitySystems;

public sealed class LightCollideSystem : EntitySystem
{
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;
    [Dependency] private readonly SlimPoweredLightSystem _lights = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LightOnCollideColliderComponent, PreventCollideEvent>(OnPreventCollide);
        SubscribeLocalEvent<LightOnCollideColliderComponent, StartCollideEvent>(OnStart);
        SubscribeLocalEvent<LightOnCollideColliderComponent, EndCollideEvent>(OnEnd);

        SubscribeLocalEvent<LightOnCollideColliderComponent, ComponentShutdown>(OnCollideShutdown);
    }

    private void OnCollideShutdown(Entity<LightOnCollideColliderComponent> ent, ref ComponentShutdown args)
    {
        // TODO: Check this on the event.
        if (TerminatingOrDeleted(ent.Owner))
            return;
    }

    // You may be wondering what de fok this is doing here.
    // At the moment there's no easy way to do collision whitelists based on components.
    private void OnPreventCollide(Entity<LightOnCollideColliderComponent> ent, ref PreventCollideEvent args)
    {
        if (!HasComp<LightOnCollideComponent>(args.OtherEntity))
        {
            args.Cancelled = true;
        }
    }

    private void OnEnd(Entity<LightOnCollideColliderComponent> ent, ref EndCollideEvent args)
    {
        if (args.OurFixtureId != ent.Comp.FixtureId)
            return;

        if (!HasComp<LightOnCollideComponent>(args.OtherEntity))
            return;

        _lights.SetEnabled(args.OtherEntity, false);
    }

    private void OnStart(Entity<LightOnCollideColliderComponent> ent, ref StartCollideEvent args)
    {
        if (args.OurFixtureId != ent.Comp.FixtureId)
            return;

        if (!HasComp<LightOnCollideComponent>(args.OtherEntity))
            return;

        _lights.SetEnabled(args.OtherEntity, true);
    }
}
