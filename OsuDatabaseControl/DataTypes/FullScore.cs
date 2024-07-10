using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.Common;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes.Common;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Interfaces;
using OsuDatabaseControl.Utils;

namespace OsuDatabaseControl.DataTypes
{
    public class FullScore : ICloneable
    {
        public string PlayerName { get; set; }
        public PlayMode PlayMode { get; set; }
        public Mods Mods { get; set; }
        public string ArtistName { get; set; }
        public string ArtistNameUnicode { get; set; }
        public string SongTitle { get; set; }
        public string SongTitleUnicode { get; set; }
        public string CreatorName { get; set; }
        public string DifficultyName { get; set; }
        public string AudioFileName { get; set; }
        public string MD5Hash { get; set; }
        public string FolderName { get; set; }
        public string OsuFileName { get; set; }
        public BeatmapRankedStatus RankedStatus { get; set; }
        public short NumberOfHitCircles { get; set; }
        public short NumberOfSliders { get; set; }
        public short NumberOfSpinners { get; set; }
        public float ApproachRate { get; set; } // TODO: Byte if the version is less than 20140609, Single otherwise.
        public float CircleSize { get; set; }
        public float HPDrain { get; set; }
        public float OverallDifficulty { get; set; }
        public double SliderVelocity { get; set; }
        public int DrainTime { get; set; }
        public int TotalTime { get; set; }
        public string SongSource { get; set; }
        public string SongTags { get; set; }
        public short OnlineOffset { get; set; }
        public long LastPlayedTime { get; set; }
        public double StarRating { get; set; }
        public short C300 { get; set; }
        public short C100 { get; set; }
        public short C50 { get; set; }
        public short Miss { get; set; }
        public short Katu { get; set; }
        public short Geki { get; set; }
        public int TotalScore { get; set; }
        public short MaxCombo { get; set; }
        public bool Perfect { get; set; }
        public DateTime Date { get; set; }

        // BPM
        public double BPM { get; set; }
        public double LowestBPM { get; set; }
        public double HighestBPM { get; set; }

        public int TotalNumberOfObjects => NumberOfSliders + NumberOfHitCircles + NumberOfSpinners;

        public double Accuracy
        {
            get
            {
                switch (PlayMode)
                {
                    case PlayMode.Osu:
                        return (C300 * 300.0 + C100 * 100.0 + C50 * 50.0) / ((C300 + C100 + C50 + Miss) * 300.0);
                    case PlayMode.Taiko:
                        return (C300 + C100 * 0.5) / (double)(C300 + C100 + Miss);
                    case PlayMode.OsuMania:
                        return ((C300 + Geki) * 300.0 + Katu * 200.0 + C100 * 100.0 + C50 * 50.0) /
                                ((C300 + Geki + Katu + C100 + C50 + Miss) * 300.0);
                    case PlayMode.CatchTheBeat:
                        return (C300 + C100 + C50) / (double)(C300 + C100 + C50 + Katu + Miss);
                    default:
                        return 0;
                }
            }
        }


        public FullScore(Score score, BeatmapDictionary beatmapDict)
        {
            Beatmap beatmap = beatmapDict.GetBeatmap(score.MapHash);

            PlayerName = score.PlayerName;
            PlayMode = score.PlayMode;
            Mods = (Mods)score.Mods;
            C300 = score.C300;
            C100 = score.C100;
            C50 = score.C50;
            Miss = score.Miss;
            Katu = score.Katu;
            Geki = score.Geki;
            TotalScore = score.TotalScore;
            MaxCombo = score.MaxCombo;
            Perfect = score.Perfect;
            Date = score.Date;

            SongTitle = beatmap.SongTitle;
            ArtistName = beatmap.ArtistName;
            ArtistNameUnicode = beatmap.ArtistNameUnicode;
            SongTitleUnicode = beatmap.SongTitleUnicode;
            CreatorName = beatmap.CreatorName;
            DifficultyName = beatmap.Difficulty;
            AudioFileName = beatmap.AudioFileName;
            MD5Hash = beatmap.MD5Hash;
            OsuFileName = beatmap.OsuFileName;
            FolderName = beatmap.FolderName;
            RankedStatus = beatmap.RankedStatus;
            NumberOfHitCircles = beatmap.NumberOfHitCircles;
            NumberOfSliders = beatmap.NumberOfSliders;
            NumberOfSpinners = beatmap.NumberOfSpinners;

            if (Mods.HasFlag(Mods.Hr))
            {
                ApproachRate = float.Min(beatmap.ApproachRate * ModsConstants.HR_AR_MULTIPILER, 10.0f);
                CircleSize = float.Min(beatmap.CircleSize * ModsConstants.HR_CS_MULTIPILER, 10.0f);
                HPDrain = float.Min(beatmap.HPDrain * ModsConstants.HR_HP_MULTIPILER, 10.0f);
                OverallDifficulty = float.Min(beatmap.OverallDifficulty * ModsConstants.HR_OD_MULTIPILER, 10.0f);
            }
            else if (Mods.HasFlag(Mods.Ez))
            {
                ApproachRate = beatmap.ApproachRate * ModsConstants.EZ_AR_MULTIPILER;
                CircleSize = beatmap.CircleSize * ModsConstants.EZ_CS_MULTIPILER;
                HPDrain = beatmap.HPDrain * ModsConstants.EZ_HP_MULTIPILER;
                OverallDifficulty = beatmap.OverallDifficulty * ModsConstants.EZ_OD_MULTIPILER;
            }
            else
            {
                ApproachRate = beatmap.ApproachRate;
                CircleSize = beatmap.CircleSize;
                HPDrain = beatmap.HPDrain;
                OverallDifficulty = beatmap.OverallDifficulty;
            }

            SliderVelocity = beatmap.SliderVelocity;
            DrainTime = beatmap.DrainTime;
            TotalTime = beatmap.TotalTime;
            SongSource = beatmap.SongSource;
            SongTags = beatmap.SongTags;
            OnlineOffset = beatmap.OnlineOffset;
            LastPlayedTime = beatmap.LastPlayedTime;


            switch (beatmap.GameplayMode)
            {
                case PlayMode.Osu:
                    StarRating = findStarRating(beatmap.StarRatingStandard, Mods.MaskOnlyDifficultyChanging());
                    break;
                case PlayMode.Taiko:
                    StarRating = findStarRating(beatmap.StarRatingTaiko, Mods.MaskOnlyDifficultyChanging());
                    break;
                case PlayMode.CatchTheBeat:
                    StarRating = findStarRating(beatmap.StarRatingCTB, Mods.MaskOnlyDifficultyChanging());
                    break;
                case PlayMode.OsuMania:
                    StarRating = findStarRating(beatmap.StarRatingMania, Mods.MaskOnlyDifficultyChanging());
                    break;
                default:
                    StarRating = 0.0;
                    break;
            }

            LowestBPM = beatmap.TimingPoints.Where(tp => tp.Uninherited).Min(tp => 1 / tp.BPM * 60000);
            HighestBPM = beatmap.TimingPoints.Where(tp => tp.Uninherited).Max(tp => 1 / tp.BPM * 60000);

            // get most common value of BPM
            BPM = beatmap.TimingPoints
                .Where(tp => tp.Uninherited)
                .GroupBy(tp => 1 / tp.BPM * 60000)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();


            // Speed changing mods:
            if ((Mods & Mods.SpeedChanging) != 0)
            {
                float speedMultiplier = Mods.HasFlag(Mods.Ht)
                    ? ModsConstants.HT_SPEED_MULTIPILER
                    : ModsConstants.DT_SPEED_MULTIPILER;

                OverallDifficulty =
                    DifficultySpeedChangeConverter.GetModifiedOverallDifficulty(OverallDifficulty, speedMultiplier);
                ApproachRate = DifficultySpeedChangeConverter.GetModifiedApproachRate(ApproachRate, speedMultiplier);
                DrainTime = (int)(DrainTime / speedMultiplier);
                TotalTime = (int)(TotalTime / speedMultiplier);
                BPM = BPM * speedMultiplier;
                LowestBPM = LowestBPM * speedMultiplier;
                HighestBPM = HighestBPM * speedMultiplier;
            }
        }

        private double findStarRating(IntDoublePair[] starRating, Mods mods)
        {
            foreach (IntDoublePair rating in starRating)
            {
                if (rating.IntValue == (int)mods)
                {
                    return rating.DoubleValue;
                }
            }

            return 0.0;
        }

        public override string ToString()
        {
            return $"{Mods.ToAcronyms()}" +
                   $"[{PlayMode}] {ArtistName} - " +
                   $"{SongTitle}  [{DifficultyName}] " +
                   $"({CreatorName}, {StarRating:F2}*)" +
                   $"Played on {Date}\n" +
                   $"Total Score: {TotalScore}\n" +
                   $"300s: {C300}\n100s: {C100}\n50s: " +
                   $"{C50}\nMiss: {Miss}\nMax Combo: {MaxCombo} " +
                   $"{(Perfect ? "(PFC)" : "")}";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}