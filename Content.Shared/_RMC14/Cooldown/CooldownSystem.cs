using Content.Shared.Actions;
using Content.Shared.Actions.Events;

namespace Content.Shared._RMC14.Cooldown;

public sealed class CooldownSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<ActionSharedCooldown2Component, ActionPerformedEvent>(OnActionPerformed);
    }

    private void OnActionPerformed(Entity<ActionSharedCooldown2Component> ent, ref ActionPerformedEvent args)
    {
        if (!_actions.TryGetActionData(ent, out var action) ||
            action.UseDelay is not { } delay ||
            delay <= TimeSpan.Zero)
        {
            return;
        }

        foreach (var (id, _) in _actions.GetActions(args.Performer))
        {
            if (TryComp(id, out ActionSharedCooldown2Component? other) &&
                ent.Comp.Id == other.Id)
            {
                _actions.SetIfBiggerCooldown(id, delay);
            }
        }
    }
}
