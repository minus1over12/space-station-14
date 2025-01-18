namespace Content.Server.Cult.Components;

[RegisterComponent]
[Access(typeof(ElderGodSystem))]
public sealed partial class ElderGodComponent : Component
{
    [DataField]
    public string AlertLevelOnSpawn = default!;
}
