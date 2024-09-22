using Content.Shared._RMC14.Armor;
using Content.Shared.Damage;
using Content.Shared.Explosion;
using Content.Shared.Popups;
using Content.Shared.Coordinates;
using Robust.Shared.Network;
using Robust.Shared.Timing;
using Robust.Shared.Player;

namespace Content.Shared._RMC14.Shields;

public sealed partial class CrusherShieldSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CrusherShieldComponent, DamageModifyEvent>(OnDamage, before: [typeof(XenoShieldSystem)], after: [typeof(CMArmorSystem)]);
        SubscribeLocalEvent<CrusherShieldComponent, GetExplosionResistanceEvent>(OnGetExplosionResistance, after: [typeof(CMArmorSystem)]);
        SubscribeLocalEvent<CrusherShieldComponent, RemovedShieldEvent>(OnShieldRemove);
    }


    public void ApplyEffects(Entity<CrusherShieldComponent> ent)
    {
        if (!TryComp<CMArmorComponent>(ent, out var armor))
            return;

        ent.Comp.ExplosionOffAt = _timing.CurTime + ent.Comp.ExplosionResistanceDuration;
        ent.Comp.ShieldOffAt = _timing.CurTime + ent.Comp.ShieldDuration;
        ent.Comp.ExplosionResistApplying = true;

    }

    public void OnShieldRemove(Entity<CrusherShieldComponent> ent, ref RemovedShieldEvent args)
    {
        if (args.Type == XenoShieldSystem.ShieldType.Crusher)
            _popup.PopupEntity(Loc.GetString("rmc-xeno-defensive-shield-end"), ent, ent, PopupType.MediumCaution);
    }

    public void OnDamage(Entity<CrusherShieldComponent> ent, ref DamageModifyEvent args)
    {
        if (!TryComp<XenoShieldComponent>(ent, out var shield))
            return;

        if (!shield.Active || shield.Shield != XenoShieldSystem.ShieldType.Crusher)
            return;

        foreach (var type in args.Damage.DamageDict)
        {
            if (args.Damage.DamageDict[type.Key] <= 0)
                continue;

            args.Damage.DamageDict[type.Key] -= ent.Comp.DamageReduction;

            if (args.Damage.DamageDict[type.Key] < 0)
                args.Damage.DamageDict[type.Key] = 0;
        }
    }

    public void OnGetExplosionResistance(Entity<CrusherShieldComponent> ent, ref GetExplosionResistanceEvent args)
    {
        if (!ent.Comp.ExplosionResistApplying)
            return;

        // TODO RMC14 halved like armor for now
        var explosionResist = ent.Comp.ExplosionResistance / 2;

        var resist = (float) Math.Pow(1.1, explosionResist / 5.0); // From armor calcualtion
        args.DamageCoefficient /= resist;
    }
}
