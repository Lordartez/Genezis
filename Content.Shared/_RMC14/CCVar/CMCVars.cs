﻿using Robust.Shared;
using Robust.Shared.Configuration;

namespace Content.Shared._RMC14.CCVar;

[CVarDefs]
public sealed class CMCVars : CVars
{
    public static readonly CVarDef<float> CMXenoDamageDealtMultiplier =
        CVarDef.Create("cm.xeno_damage_dealt_multiplier", 1f, CVar.REPLICATED | CVar.SERVER);

    public static readonly CVarDef<float> CMXenoDamageReceivedMultiplier =
        CVarDef.Create("cm.xeno_damage_received_multiplier", 1f, CVar.REPLICATED | CVar.SERVER);

    public static readonly CVarDef<float> CMXenoSpeedMultiplier =
        CVarDef.Create("cm.xeno_speed_multiplier", 1f, CVar.REPLICATED | CVar.SERVER);

    public static readonly CVarDef<bool> CMPlayVoicelinesArachnid =
        CVarDef.Create("cm.play_voicelines_arachnid", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesDiona =
        CVarDef.Create("cm.play_voicelines_diona", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesDwarf =
        CVarDef.Create("cm.play_voicelines_dwarf", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesFelinid =
        CVarDef.Create("cm.play_voicelines_felinid", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesHuman =
        CVarDef.Create("cm.play_voicelines_human", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesMoth =
        CVarDef.Create("cm.play_voicelines_moth", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesReptilian =
        CVarDef.Create("cm.play_voicelines_reptilian", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<bool> CMPlayVoicelinesSlime =
        CVarDef.Create("cm.play_voicelines_slime", true, CVar.REPLICATED | CVar.CLIENT | CVar.ARCHIVE);

    public static readonly CVarDef<int> CMMaxHeavyAttackTargets =
        CVarDef.Create("cm.max_heavy_attack_targets", 3, CVar.REPLICATED | CVar.SERVER);

}
