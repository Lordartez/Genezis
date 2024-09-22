using Robust.Shared.Network;
using Robust.Shared.Physics.Events;

namespace Content.Shared._RMC14.Projectiles;

public sealed class CMProjectileSystem : EntitySystem
{
    [Dependency] private readonly INetManager _net = default!;

    private void OnDeleteOnCollideStartCollide(Entity<DeleteOnCollideComponent> ent, ref StartCollideEvent args)
    {
        if (_net.IsServer)
            QueueDel(ent);
    }
}
