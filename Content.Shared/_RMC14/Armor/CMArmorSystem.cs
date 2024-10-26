using Content.Shared.Alert;
using Content.Shared.Clothing.Components;
using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Explosion;
using Content.Shared.FixedPoint;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Robust.Shared.Prototypes;

namespace Content.Shared._RMC14.Armor;

public sealed class CMArmorSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alerts = default!;
    [Dependency] private readonly InventorySystem _inventory = default!;
    [Dependency] private readonly IPrototypeManager _prototypes = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;

    private static readonly ProtoId<DamageGroupPrototype> ArmorGroup = "Brute";
    private static readonly ProtoId<DamageGroupPrototype> BioGroup = "Burn";

    public override void Initialize()
    {
        SubscribeLocalEvent<CMArmorComponent, DamageModifyEvent>(OnDamageModify);
        SubscribeLocalEvent<CMArmorComponent, CMGetArmorEvent>(OnGetArmor);
        SubscribeLocalEvent<CMArmorComponent, InventoryRelayedEvent<CMGetArmorEvent>>(OnGetArmorRelayed);
        SubscribeLocalEvent<CMArmorComponent, InventoryRelayedEvent<GetExplosionResistanceEvent>>(OnGetExplosionResistanceRelayed);
        SubscribeLocalEvent<CMArmorComponent, GetExplosionResistanceEvent>(OnGetExplosionResistance);
        SubscribeLocalEvent<CMArmorComponent, GotEquippedEvent>(OnGotEquipped);

        SubscribeLocalEvent<CMArmorUserComponent, DamageModifyEvent>(OnUserDamageModify);

        SubscribeLocalEvent<CMArmorPiercingComponent, CMGetArmorPiercingEvent>(OnPiercingGetArmor);

        SubscribeLocalEvent<InventoryComponent, CMGetArmorEvent>(_inventory.RelayEvent);

        SubscribeLocalEvent<ClothingBlockBackpackComponent, BeingEquippedAttemptEvent>(OnBlockBackpackEquippedAttempt);
        SubscribeLocalEvent<ClothingBlockBackpackComponent, InventoryRelayedEvent<RMCEquipAttemptEvent>>(OnBlockBackpackEquipAttempt);

        SubscribeLocalEvent<ClothingComponent, BeingEquippedAttemptEvent>(OnClothingEquippedAttempt);

        SubscribeLocalEvent<InventoryComponent, RMCEquipAttemptEvent>(_inventory.RelayEvent);
    }

    private void OnDamageModify(Entity<CMArmorComponent> armored, ref DamageModifyEvent args)
    {
        ModifyDamage(armored, ref args);
    }

    private void OnGetArmor(Entity<CMArmorComponent> armored, ref CMGetArmorEvent args)
    {
        args.Armor += armored.Comp.Armor;
        args.Bio += armored.Comp.Bio;
    }

    private void OnGetArmorRelayed(Entity<CMArmorComponent> armored, ref InventoryRelayedEvent<CMGetArmorEvent> args)
    {
        args.Args.Armor += armored.Comp.Armor;
        args.Args.Bio += armored.Comp.Bio;
    }

    private void OnGetExplosionResistanceRelayed(Entity<CMArmorComponent> ent, ref InventoryRelayedEvent<GetExplosionResistanceEvent> args)
    {
        // TODO RMC14 unhalve this when we can calculate explosion damage better
        var armor = ent.Comp.ExplosionArmor / 2;

        if (armor <= 0)
            return;

        var resist = (float) Math.Pow(1.1, armor / 5.0);
        args.Args.DamageCoefficient /= resist;
    }

    private void OnGetExplosionResistance(Entity<CMArmorComponent> armored, ref GetExplosionResistanceEvent args)
    {
        // TODO RMC14 unhalve this when we can calculate explosion damage better
        var armor = armored.Comp.ExplosionArmor / 2;

        if (armor <= 0)
            return;

        var resist = (float) Math.Pow(1.1, armor / 5.0);
        args.DamageCoefficient /= resist;
    }

    private void OnGotEquipped(Entity<CMArmorComponent> armored, ref GotEquippedEvent args)
    {
        EnsureComp<CMArmorUserComponent>(args.Equipee);
    }

    private void OnUserDamageModify(Entity<CMArmorUserComponent> ent, ref DamageModifyEvent args)
    {
        ModifyDamage(ent, ref args);
    }

    private void OnPiercingGetArmor(Entity<CMArmorPiercingComponent> piercing, ref CMGetArmorPiercingEvent args)
    {
        args.Piercing += piercing.Comp.Amount;
    }

    private void OnBlockBackpackEquippedAttempt(Entity<ClothingBlockBackpackComponent> ent, ref BeingEquippedAttemptEvent args)
    {
        if (args.Cancelled)
            return;

        var slots = _inventory.GetSlotEnumerator(args.EquipTarget, SlotFlags.BACK);
        while (slots.MoveNext(out var slot))
        {
            if (slot.ContainedEntity == null)
                continue;

            if (HasComp<ClothingIgnoreBlockBackpackComponent>(slot.ContainedEntity))
                return;

            args.Cancel();
            args.Reason = "rmc-block-backpack-cant-other";
            break;
        }
    }

    private void OnBlockBackpackEquipAttempt(Entity<ClothingBlockBackpackComponent> ent, ref InventoryRelayedEvent<RMCEquipAttemptEvent> args)
    {
        ref readonly var ev = ref args.Args.Event;
        if (ev.Cancelled)
            return;

        if (HasComp<ClothingIgnoreBlockBackpackComponent>(args.Args.Event.Equipment))
            return;

        if ((ev.SlotFlags & SlotFlags.BACK) == 0)
            return;

        ev.Cancel();
        ev.Reason = "rmc-block-backpack-cant-backpack";
    }

    private void OnClothingEquippedAttempt(Entity<ClothingComponent> ent, ref BeingEquippedAttemptEvent args)
    {
        if (args.Cancelled)
            return;

        var ev = new RMCEquipAttemptEvent(args, SlotFlags.All);
        RaiseLocalEvent(args.EquipTarget, ref ev);
    }

    private void ModifyDamage(EntityUid ent, ref DamageModifyEvent args)
    {
        // TODO RMC14 the slot should depend on the part that is receiving the damage once part damage is in
        var ev = new CMGetArmorEvent(SlotFlags.OUTERCLOTHING | SlotFlags.INNERCLOTHING);
        RaiseLocalEvent(ent, ref ev);

        var armorPiercing = args.ArmorPiercing;
        if (args.Tool != null)
        {
            var piercingEv = new CMGetArmorPiercingEvent(ent);
            RaiseLocalEvent(args.Tool.Value, ref piercingEv);
            armorPiercing += piercingEv.Piercing;
        }

        ev.Armor -= armorPiercing;
        ev.Bio -= armorPiercing;

        if (args.Origin is { } origin)
        {
            var originCoords = _transform.GetMapCoordinates(origin);
            var armorCoords = _transform.GetMapCoordinates(ent);

            if (originCoords.MapId == armorCoords.MapId)
            {
                var diff = (originCoords.Position - armorCoords.Position).ToWorldAngle().GetCardinalDir();
                if (diff == _transform.GetWorldRotation(ent).GetCardinalDir())
                {
                    ev.Armor += ev.FrontalArmor;
                }
            }
        }

        args.Damage = new DamageSpecifier(args.Damage);
        Resist(args.Damage, ev.Armor, ArmorGroup);
        Resist(args.Damage, ev.Bio, BioGroup);
    }

    private void Resist(DamageSpecifier damage, int armor, ProtoId<DamageGroupPrototype> group)
    {
        armor = Math.Max(armor, 0);
        if (armor <= 0)
            return;

        var resist = Math.Pow(1.1, armor / 5.0);
        var types = _prototypes.Index(group).DamageTypes;

        foreach (var type in types)
        {
            if (damage.DamageDict.TryGetValue(type, out var amount) &&
                amount > FixedPoint2.Zero)
            {
                damage.DamageDict[type] = amount / resist;
            }
        }

        var newDamage = damage.GetTotal();
        if (newDamage != FixedPoint2.Zero && newDamage < armor * 2)
        {
            var damageWithArmor = FixedPoint2.Max(0, newDamage * 4 - armor);

            foreach (var type in types)
            {
                if (damage.DamageDict.TryGetValue(type, out var amount) &&
                    amount > FixedPoint2.Zero)
                {
                    damage.DamageDict[type] = amount * damageWithArmor / (newDamage * 4);
                }
            }
        }
    }

    public void SetArmorPiercing(Entity<CMArmorPiercingComponent> ent, int amount)
    {
        ent.Comp.Amount = amount;
        Dirty(ent);
    }
}
