using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;

namespace OsuDatabaseControl.DTO
{
    public class BeatmapPrintable : ICloneable
    {
        PlayMode GameplayMode {  get; set; }
        string ArtistName {  get; set; }
        string ArtistNameUnicode { get; set; }
        string SongTitle { get; set; }
        string SongTitleUnicode { get; set; }
        string Difficulty { get; set; }
        string CreatorName { get; set; }
        double StarRating { get; set; }
        public BeatmapPrintable(Beatmap beatmap) {
            GameplayMode = beatmap.GameplayMode;
            ArtistName = beatmap.ArtistName;
            ArtistNameUnicode = beatmap.ArtistNameUnicode;
            SongTitle = beatmap.SongTitle;
            SongTitleUnicode = beatmap.SongTitleUnicode;
            Difficulty = beatmap.Difficulty;
            CreatorName = beatmap.CreatorName;
            switch (GameplayMode)
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
        }

        public override string ToString()
        {
            return $"[{GameplayMode}] {ArtistName} ({ArtistNameUnicode}) - " +
            $"{SongTitle} ({SongTitleUnicode}) [{Difficulty}] " +
            $"({CreatorName}, {StarRating.ToString("F2")}*)";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
