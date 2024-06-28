using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;

namespace OsuDatabaseControl.Performance.Interfaces
{
    public interface IPerfomanceCalc
    {
        public float ComputeEffectiveMissCount(Beatmap beatmap, Score score);
        public float ComputeAimValue(Beatmap beatmap, Score score);
        public float ComputeSpeedValue(Beatmap beatmap, Score score);
	    public float ComputeAccuracyValue(Beatmap beatmap, Score score);
	    public float ComputeFlashlightValue(Beatmap beatmap, Score score);
        public float GetComboScalingFactor(Beatmap beatmap, Score score);
        public float ComputeTotalValue(Beatmap beatmap, Score score);
    }
}
