using Content.Shared.Damage.Components;
using Content.Shared.Throwing;

namespace Content.Shared._RMC14.Throwing;

public sealed class ThrowingSystem : EntitySystem
{
    [Dependency] private readonly ThrownItemSystem _thrown = default!;

    private EntityQuery<ThrownItemComponent> _thrownItemQuery;

    public override void Initialize()
    {
        _thrownItemQuery = GetEntityQuery<ThrownItemComponent>();

        SubscribeLocalEvent<ThrownLimitHitsComponent, ThrowDoHitEvent>(OnThrownLimitHitsDoHit);
        SubscribeLocalEvent<ThrownLimitHitsComponent, LandEvent>(OnThrownLimitHitsLand);
        SubscribeLocalEvent<ThrownLimitHitsComponent, StopThrowEvent>(OnThrownLimitHitsStopThrow);
    }

    private void OnThrownLimitHitsLand(Entity<ThrownLimitHitsComponent> ent, ref LandEvent args)
    {
        ent.Comp.Hit = false;
        Dirty(ent);
    }

    private void OnThrownLimitHitsDoHit(Entity<ThrownLimitHitsComponent> ent, ref ThrowDoHitEvent args)
    {
        ent.Comp.Hit = true;
        Dirty(ent);

        if (_thrownItemQuery.TryComp(ent, out var thrown))
            _thrown.StopThrow(ent, thrown);
    }

    private void OnThrownLimitHitsStopThrow(Entity<ThrownLimitHitsComponent> ent, ref StopThrowEvent args)
    {
        RemCompDeferred<ThrownLimitHitsComponent>(ent);
    }
}
