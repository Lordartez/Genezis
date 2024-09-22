using Content.Server.Chat.Systems;
using Content.Shared._RMC14;

namespace Content.Server._RMC14.OnCollide;

public sealed class OnCollideSystem
{
    [Dependency] private readonly ChatSystem _chat = default!;

}
