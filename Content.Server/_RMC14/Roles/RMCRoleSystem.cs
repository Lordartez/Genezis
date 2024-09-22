using Content.Server.GameTicking;

namespace Content.Server._RMC14.Roles;

public sealed class RMCRoleSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<PlayerSpawnCompleteEvent>(OnPlayerSpawnComplete);
    }

    private void OnPlayerSpawnComplete(PlayerSpawnCompleteEvent ev)
    {

    }
}
