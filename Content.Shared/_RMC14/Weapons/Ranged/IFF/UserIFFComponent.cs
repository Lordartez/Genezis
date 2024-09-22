﻿using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RMC14.Weapons.Ranged.IFF;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(GunIFFSystem))]
public sealed partial class UserIFFComponent : Component
{
    [DataField, AutoNetworkedField]
    public EntProtoId<IFFFactionComponent>? Faction;
}
