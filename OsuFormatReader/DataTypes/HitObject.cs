using OsuFormatReader.Enums;

namespace OsuFormatReader.DataTypes;

public class HitObject
{
    public int x { get; set; }
    public int y { get; set; }
    public int time { get; set; }
    public int type { get; set; }
    public HitSound hitSound  { get; set; }
    public int objectParams  { get; set; }
    public HitSample hitSample  { get; set; }
}