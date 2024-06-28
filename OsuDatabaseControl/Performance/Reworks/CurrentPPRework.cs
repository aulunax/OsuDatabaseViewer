using OsuDatabaseControl.Performance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;

namespace OsuDatabaseControl.Performance.Reworks
{
    public class CurrentPPRework : IPerfomanceCalc
    {
        public float ComputeAccuracyValue(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }

        public float ComputeAimValue(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }

        public float ComputeEffectiveMissCount(Beatmap beatmap, Score score)
        {
            float comboBasedMissCount = 0.0f;
            //float beatmapMaxCombo = beatmap.DifficultyAttribute(score.Mods, Beatmap::MaxCombo);
            //if (beatmap.NumberOfSliders > 0)
            //{
            //    float fullComboThreshold = beatmapMaxCombo - 0.1f * beatmap.NumberOfSliders;
            //    if (score.MaxCombo < fullComboThreshold)
            //        comboBasedMissCount = fullComboThreshold / Math.Max((short)1, score.MaxCombo);
            //}

            // Clamp miss count to maximum amount of possible breaks
            comboBasedMissCount = Math.Min(comboBasedMissCount, score.C100 + score.C50 + score.Miss);

            return Math.Max(score.Miss, comboBasedMissCount);
        }

        public float ComputeFlashlightValue(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }

        public float ComputeSpeedValue(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }

        public float ComputeTotalValue(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }

        public float GetComboScalingFactor(Beatmap beatmap, Score score)
        {
            throw new NotImplementedException();
        }
    }
}
