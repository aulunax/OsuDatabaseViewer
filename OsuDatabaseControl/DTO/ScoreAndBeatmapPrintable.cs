using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.DataTypes.Osu;
using Test.Enums;
using Test.Interfaces;

namespace Test.DTO
{
    public class ScoreAndBeatmapPrintable : ICloneable
    {
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
            PlayMode = beatmap.GameplayMode;
            Mods = (Mods)score.Mods;
            SongTitle = beatmap.SongTitle;
            ArtistName = beatmap.ArtistName;
            CreatorName = beatmap.CreatorName;
            DifficultyName = beatmap.Difficulty;
            switch (beatmap.GameplayMode)
            {
                case PlayMode.Osu:
                    StarRating = beatmap.StarRatingStandard[0].DoubleValue;
                    break;
                case PlayMode.Taiko:
                    StarRating = beatmap.StarRatingTaiko[0].DoubleValue;
                    break;
                case PlayMode.CatchTheBeat:
                    StarRating = beatmap.StarRatingCTB[0].DoubleValue;
                    break;
                case PlayMode.OsuMania:
                    StarRating = beatmap.StarRatingMania[0].DoubleValue;
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
