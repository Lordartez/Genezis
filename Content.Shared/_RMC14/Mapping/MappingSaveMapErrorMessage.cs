﻿using Lidgren.Network;
using Robust.Shared.Network;
using Robust.Shared.Serialization;

namespace Content.Shared._RMC14.Mapping;

public sealed class MappingSaveMapErrorMessage : NetMessage
{
    public override MsgGroups MsgGroup => MsgGroups.Command;
    public override NetDeliveryMethod DeliveryMethod => NetDeliveryMethod.ReliableUnordered;

    public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
    {
    }

    public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
    {
    }
}
