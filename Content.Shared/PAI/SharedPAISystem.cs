using Content.Shared.Actions;

namespace Content.Shared.PAI;

/// <summary>
/// pAIs, or Personal AIs, are essentially portable ghost role generators.
/// In their current implementation, they create a ghost role anyone can access,
/// and that a player can also "wipe" (reset/kick out player).
/// Theoretically speaking pAIs are supposed to use a dedicated "offer and select" system,
///  with the player holding the pAI being able to choose one of the ghosts in the round.
/// This seems too complicated for an initial implementation, though,
///  and there's not always enough players and ghost roles to justify it.
/// </summary>
public abstract class SharedPAISystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PAIComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<PAIComponent, ComponentShutdown>(OnShutdown);
    }

    private void OnMapInit(EntityUid uid, PAIComponent component, MapInitEvent args)
    {
        _actionsSystem.AddAction(uid, ref component.ShopAction, component.ShopActionId);
    }

    private void OnShutdown(EntityUid uid, PAIComponent component, ComponentShutdown args)
    {
        _actionsSystem.RemoveAction(uid, component.ShopAction);
    }
}
public sealed partial class PAIShopActionEvent : InstantActionEvent
{
}
