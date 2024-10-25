using Content.Shared.Damage;
using Content.Shared.Interaction.Events;
using Content.Shared.Stunnable;
using Content.Shared.Weapons.Melee;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Whitelist;

namespace Content.Shared._RMC14.Weapons.Melee;

public abstract class SharedRMCMeleeWeaponSystem : EntitySystem
{
    [Dependency] private readonly SharedStunSystem _stun = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelist = default!;

    private EntityQuery<MeleeWeaponComponent> _meleeWeaponQuery;

    public override void Initialize()
    {
        _meleeWeaponQuery = GetEntityQuery<MeleeWeaponComponent>();

        SubscribeLocalEvent<ImmuneToUnarmedComponent, GettingAttackedAttemptEvent>(OnImmuneToUnarmedGettingAttacked);

        SubscribeLocalEvent<ImmuneToMeleeComponent, GettingAttackedAttemptEvent>(OnImmuneToMeleeGettingAttacked);

        SubscribeLocalEvent<MeleeReceivedMultiplierComponent, DamageModifyEvent>(OnMeleeReceivedMultiplierDamageModify);

        SubscribeLocalEvent<StunOnHitComponent, MeleeHitEvent>(OnStunOnHitMeleeHit);

        SubscribeLocalEvent<MeleeDamageMultiplierComponent, MeleeHitEvent>(OnMultiplierOnHitMeleeHit);
    }

    private void OnStunOnHitMeleeHit(Entity<StunOnHitComponent> ent, ref MeleeHitEvent args)
    {
        if (!args.IsHit)
            return;

        foreach (var hit in args.HitEntities)
        {
            if (_whitelist.IsValid(ent.Comp.Whitelist, hit))
                _stun.TryParalyze(hit, ent.Comp.Duration, true);
        }
    }

    private void OnMultiplierOnHitMeleeHit(Entity<MeleeDamageMultiplierComponent> ent, ref MeleeHitEvent args)
    {
        if (!args.IsHit)
            return;

        var comp = ent.Comp;

        foreach (var hit in args.HitEntities)
        {
            if (_whitelist.IsValid(comp.Whitelist, hit))
            {
                var damage = args.BaseDamage * comp.Multiplier;
                args.BonusDamage += damage;
                break;
            }
        }
    }

    private void OnImmuneToUnarmedGettingAttacked(Entity<ImmuneToUnarmedComponent> ent, ref GettingAttackedAttemptEvent args)
    {

        if (args.Attacker == args.Weapon)
            args.Cancelled = true;
    }

    private void OnImmuneToMeleeGettingAttacked(Entity<ImmuneToMeleeComponent> ent, ref GettingAttackedAttemptEvent args)
    {
        if (_whitelist.IsWhitelistPassOrNull(ent.Comp.Whitelist, args.Attacker))
            args.Cancelled = true;
    }

    private void OnMeleeReceivedMultiplierDamageModify(Entity<MeleeReceivedMultiplierComponent> ent, ref DamageModifyEvent args)
    {
        if (!_meleeWeaponQuery.HasComp(args.Tool))
            return;

        args.Damage = args.Damage * ent.Comp.OtherMultiplier;
    }
}
