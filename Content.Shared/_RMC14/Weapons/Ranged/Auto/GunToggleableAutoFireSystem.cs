using Content.Shared._RMC14.Weapons.Ranged.Battery;
using Content.Shared.Actions;
using Content.Shared.Coordinates;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Interaction;
using Content.Shared.Interaction.Events;
using Content.Shared.Mobs.Systems;
using Content.Shared.Physics;
using Content.Shared.Popups;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Wieldable;
using Content.Shared.Wieldable.Components;
using Robust.Shared.Containers;
using Robust.Shared.Network;
using Robust.Shared.Physics.Collision.Shapes;
using Robust.Shared.Timing;

namespace Content.Shared._RMC14.Weapons.Ranged.Auto;

public sealed class GunToggleableAutoFireSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly EntityLookupSystem _entityLookup = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    [Dependency] private readonly SharedHandsSystem _hands = default!;
    [Dependency] private readonly SharedInteractionSystem _interaction = default!;
    [Dependency] private readonly MobStateSystem _mobState = default!;
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly CMGunSystem _rmcGun = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;

    public readonly PolygonShape Shape = new();
    public bool Debug;

    public override void Initialize()
    {
        SubscribeLocalEvent<GunToggleableAutoFireComponent, GetItemActionsEvent>(OnGetItemActions);
        SubscribeLocalEvent<GunToggleableAutoFireComponent, GunToggleableAutoFireActionEvent>(OnAutoFireAction);

        SubscribeLocalEvent<ActiveGunAutoFireComponent, ComponentRemove>(OnRemove);
        SubscribeLocalEvent<ActiveGunAutoFireComponent, ItemUnwieldedEvent>(OnDoRemove);
        SubscribeLocalEvent<ActiveGunAutoFireComponent, DroppedEvent>(OnDoRemove);
        SubscribeLocalEvent<ActiveGunAutoFireComponent, GunUnpoweredEvent>(OnDoRemove);
    }

    private void OnGetItemActions(Entity<GunToggleableAutoFireComponent> ent, ref GetItemActionsEvent args)
    {
        args.AddAction(ref ent.Comp.Action, ent.Comp.ActionId);
        Dirty(ent);
    }

    private void OnAutoFireAction(Entity<GunToggleableAutoFireComponent> ent, ref GunToggleableAutoFireActionEvent args)
    {
        args.Handled = true;

        var user = args.Performer;
        if (!_rmcGun.HasRequiredEquippedPopup(ent.Owner, user))
            return;

        if (!_hands.IsHolding(user, ent))
        {
            var msg = Loc.GetString("rmc-toggleable-autofire-requires-wielding", ("gun", ent));
            _popup.PopupClient(msg, user, user, PopupType.MediumCaution);
            return;
        }

        if (TryComp(ent, out WieldableComponent? wieldable) &&
            !wieldable.Wielded)
        {
            var msg = Loc.GetString("rmc-toggleable-autofire-requires-wielding", ("gun", ent));
            _popup.PopupClient(msg, user, user, PopupType.MediumCaution);
            return;
        }

        if (EnsureComp<ActiveGunAutoFireComponent>(ent, out _))
        {
            RemCompDeferred<ActiveGunAutoFireComponent>(ent);
            AutoUpdated((ent, ent), false);
            return;
        }

        AutoUpdated((ent, ent), true);
    }

    private void OnRemove(Entity<ActiveGunAutoFireComponent> ent, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(ent))
            return;

        AutoUpdated(ent.Owner, false);
    }

    private void OnDoRemove<T>(Entity<ActiveGunAutoFireComponent> ent, ref T args)
    {
        RemCompDeferred<ActiveGunAutoFireComponent>(ent);
        AutoUpdated(ent.Owner, false);
    }

    private void AutoUpdated(Entity<GunToggleableAutoFireComponent?> ent, bool active)
    {
        if (!Resolve(ent, ref ent.Comp, false))
            return;

        _actions.SetToggled(ent.Comp.Action, active);
    }

    public override void Update(float frameTime)
    {
        if (!Debug && _net.IsClient)
            return;

        var time = _timing.CurTime;
        var query = EntityQueryEnumerator<ActiveGunAutoFireComponent, GunToggleableAutoFireComponent, GunComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var active, out var auto, out var gun, out var xform))
        {
            if (time < active.NextFire)
                continue;

            active.NextFire = time + active.FailCooldown;
            if (!_container.TryGetContainingContainer((uid, xform), out var container) ||
                !_hands.IsHolding(container.Owner, uid))
            {
                RemCompDeferred<ActiveGunAutoFireComponent>(uid);
                continue;
            }

            var (pos, rotation) = _transform.GetWorldPositionRotation(xform);
            rotation = rotation.GetCardinalDir().ToAngle();
            pos = pos + rotation.ToWorldVec() * auto.Range.Y / 2;
            var box = new Box2Rotated(Box2.CenteredAround(pos, auto.Range), rotation, pos);
            var shapeTransform = Robust.Shared.Physics.Transform.Empty;

        }
    }
}
