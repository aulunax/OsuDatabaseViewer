namespace OsuDatabaseControl.Common;

public class DoubleTimeConversion
{
    public static float GetConvertedApproachRate(float ar)
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

        return getApproachRateFromPreemptTime(preempt/1.5f);
    }
    
    public static float GetConvertedOverallDifficulty(float od)
    {
        return 0.0f;
    }

    private static float getApproachRateFromPreemptTime(float preempt)
    {
        float ar = 5.0f;
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
}