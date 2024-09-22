using Content.Shared.Popups;

namespace Content.Shared._RMC14.Light;

public sealed class CMPoweredLightSystem : EntitySystem
{
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {

    }

}
