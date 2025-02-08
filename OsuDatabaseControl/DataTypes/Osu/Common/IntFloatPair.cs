namespace OsuDatabaseControl.DataTypes.Common;


public class IntFloatPair
{
    public int IntValue { get; set; }
    public float FloatValue { get; set; }
    public IntFloatPair(int IntValue, float FloatValue) {
        this.IntValue = IntValue;
        this.FloatValue = FloatValue;
    }
}
