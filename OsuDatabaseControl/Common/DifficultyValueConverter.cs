namespace OsuDatabaseControl.Common;

public class DifficultyValueConverter
{
    public static float GetApproachRateValue(float ar)
    {
        float preempt = 1200.0f;
        
        if (ar > 5)
        {
            preempt = preempt - 750.0f * (ar - 5.0f) / 5;
        }
        else if (ar < 5)
        {
            preempt = preempt + 600.0f * (5.0f - ar) / 5;
        }

        return preempt;
    }
    
    public static float GetOverallDifficultyValue(float od)
    {
        return 79.5f - 6.0f * od;
    }
}