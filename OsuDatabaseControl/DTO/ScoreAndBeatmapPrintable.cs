using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes.Common;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Interfaces;

namespace OsuDatabaseControl.DTO
{
    public class ScoreAndBeatmapPrintable : ICloneable
    {
        public string PlayerName { get; set; }
        public PlayMode PlayMode { get; set; }
        public Mods Mods { get; set; }
        public string SongTitle { get; set; }
        public string ArtistName { get; set; }
        public string CreatorName { get; set; }
        public string DifficultyName { get; set; }
        public double StarRating { get; set; }
        public short C300 { get; set; }
        public short C100 { get; set; }
        public short C50 { get; set; }
        public short Miss { get; set; }
        public int TotalScore { get; set; }
        public short MaxCombo { get; set; }
        public bool Perfect { get; set; }
        public DateTime Date { get; set; }


        public ScoreAndBeatmapPrintable(Score score, BeatmapDictionary beatmapDict)
        {
            Beatmap beatmap = beatmapDict.GetBeatmap(score.MapHash);
            PlayerName = score.PlayerName;
            PlayMode = score.PlayMode;
            Mods = (Mods)score.Mods;
            SongTitle = beatmap.SongTitle;
            ArtistName = beatmap.ArtistName;
            CreatorName = beatmap.CreatorName;
            DifficultyName = beatmap.Difficulty;
            
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
            C300 = score.C300;
            C100 = score.C100;
            C50 = score.C50;
            Miss = score.Miss;
            TotalScore = score.TotalScore;
            MaxCombo = score.MaxCombo;
            Perfect = score.Perfect;
            Date = score.Date;
        }

        private double findStarRating(IntDoublePair[] starRating, Mods mods)
        {
            foreach (IntDoublePair rating in starRating)
            {
                if (rating.IntValue == (int)mods) { 
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
                $"({CreatorName}, {StarRating.ToString("F2")}*)" +
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
