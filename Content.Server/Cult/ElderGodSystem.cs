using Content.Server.AlertLevel;
using Content.Server.Cult.Components;
using Content.Server.RoundEnd;
using Content.Server.Station.Systems;

namespace Content.Server.Cult;

public sealed class ElderGodSystem : EntitySystem
{
    [Dependency] private readonly AlertLevelSystem _alertLevel = default!;
    [Dependency] private readonly RoundEndSystem _roundEndSystem = default!;
    [Dependency] private readonly StationSystem _station = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ElderGodComponent, MapInitEvent>(OnInit);
    }

    private void OnInit(EntityUid uid, ElderGodComponent component, MapInitEvent args)
    {
        var godXform = Transform(uid);
        var stationUid = _station.GetStationInMap(godXform.MapID);
        // The god may not be on a station, so it's more important to just
        // let people know that a god was summoned in their vicinity instead.
        // Otherwise, you could set every station to whatever AlertLevelOnActivate is.
        if (stationUid != null)
            _alertLevel.SetLevel(stationUid.Value, component.AlertLevelOnSpawn, true, true, true, true);
        _roundEndSystem.RequestRoundEnd(TimeSpan.FromMinutes(2), null, false);
    }
}
