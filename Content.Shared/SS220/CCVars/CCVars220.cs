using Robust.Shared.Configuration;

namespace Content.Shared.SS220.CCVars;

[CVarDefs]
public sealed class CCVars220
{

    /// <summary>
    /// How Round End Titles are shown for player
    /// </summary>
    public static readonly CVarDef<RoundEndTitlesMode> RoundEndTitlesOpenMode =
        CVarDef.Create("round_end_titles.open_mode", RoundEndTitlesMode.Fullscreen, CVar.CLIENTONLY | CVar.ARCHIVE);

    /// <summary>
    ///     Delay in seconds before first load of the discord sponsors data.
    /// </summary>
    public static readonly CVarDef<float> DiscordSponsorsCacheLoadDelaySeconds =
        CVarDef.Create("discord_sponsors_cache.load_delay_seconds", 10f, CVar.SERVERONLY);

    /// <summary>
    ///     Interval in seconds between refreshes of the discord sponsors data.
    /// </summary>
    public static readonly CVarDef<float> DiscordSponsorsCacheRefreshIntervalSeconds =
        CVarDef.Create("discord_sponsors_cache.refresh_interval_seconds", 60f * 60f * 4f, CVar.SERVERONLY);
}
