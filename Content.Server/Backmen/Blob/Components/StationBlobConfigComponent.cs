﻿using System.ComponentModel.DataAnnotations;
using Content.Server.Backmen.SpecForces;
using Robust.Shared.Prototypes;

namespace Content.Server.Backmen.Blob.Components;

[RegisterComponent]
public sealed partial class StationBlobConfigComponent : Component
{
    public const int DefaultStageEnd = 800;

    [DataField("stageBegin")]
    public int StageBegin { get; set; } = 30;

    [DataField("stageCritical")]
    public int StageCritical { get; set; } = 400;

    [DataField("stageTheEnd")]
    public int StageTheEnd { get; set; } = DefaultStageEnd;

    [DataField("specForceTeam")]
    public ProtoId<SpecForceTeamPrototype> SpecForceTeam { get; set; } = "RXBZZBlobDefault";
}
