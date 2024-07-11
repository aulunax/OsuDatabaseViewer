namespace OsuDatabaseControl.Enums.Display;

/// <summary>
/// Used to determine which columns are visible in the main view DataGrid.
/// </summary>
[Flags]
public enum MainColumnVisibility
{
    Mode = 1 << 0,
    Artist = 1 << 1,
    Title = 1 << 2,
    Difficulty = 1 << 3,
    Creator = 1 << 4,
    Stars = 1 << 5,
    Mods = 1 << 6,
    Accuracy = 1 << 7,
    C300 = 1 << 8,
    C100 = 1 << 9,
    C50 = 1 << 10,
    Miss = 1 << 11,
    Score = 1 << 12,
    Combo = 1 << 13,
    Date = 1 << 14,
    BPM = 1 << 15,
    AR = 1 << 16,
    OD = 1 << 17,
    CS = 1 << 18,
    HP = 1 << 19,
    Length = 1 << 20,
    
    Default = All ^ BPM ^ AR ^ OD ^ CS ^ HP ^ Length,
    All = Mode | Artist | Title | Difficulty | Creator | Stars | Mods | Accuracy | C300 | C100 | C50 | Miss | Score |
          Combo | Date | BPM | AR | OD | CS | HP | Length
}