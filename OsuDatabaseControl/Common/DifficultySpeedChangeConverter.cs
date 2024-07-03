namespace OsuDatabaseControl.Common;

public class DifficultySpeedChangeConverter
{
    public static float GetModifiedApproachRate(float ar, float modifier)
    {
        float preempt = DifficultyValueConverter.GetApproachRateValue(ar);

        preempt /= modifier;
        
        if (preempt < 1200.0f)
        {
            ar = 13.0f - preempt / 150.0f;
        }
        else if (preempt > 1200.0f)
        {
            ar = 5.0f - (preempt - 1200.0f) / 120.0f ;
        }
        
        return ar;
    }
    
    public static float GetModifiedOverallDifficulty(float od, float modifier)
    {
        float hitwindow = DifficultyValueConverter.GetOverallDifficultyValue(od);

        hitwindow /= modifier;
        
        return (79.5f - hitwindow) / 6.0f;
    }
}