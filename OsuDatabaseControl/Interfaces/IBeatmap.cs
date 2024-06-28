using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataTypes.Common;
using Test.Enums;

namespace Test.Interfaces
{
    public interface IBeatmap
    {
        int? SizeInBytes { get; set; }
        string ArtistName { get; set; }
        string ArtistNameUnicode { get; set; }
        string SongTitle { get; set; }
        string SongTitleUnicode { get; set; }
        string CreatorName { get; set; }
        string Difficulty { get; set; }
        string AudioFileName { get; set; }
        string MD5Hash { get; set; }
        string OsuFileName { get; set; }
        BeatmapRankedStatus RankedStatus { get; set; }
        short NumberOfHitCircles { get; set; }
        short NumberOfSliders { get; set; }
        short NumberOfSpinners { get; set; }
        long LastModificationTime { get; set; }
        float ApproachRate { get; set; } // TODO: Byte if the version is less than 20140609, Single otherwise.
        float CircleSize { get; set; } 
        float HPDrain { get; set; } 
        float OverallDifficulty { get; set; } 
        double SliderVelocity { get; set; }
        IntDoublePair[] StarRatingStandard { get; set; } // TODO: An Int indicating the number of following Int-Double pairs, then the aforementioned pairs. Star Rating info for osu! standard, in each pair, the Int is the mod combination, and the Double is the Star Rating. Only present if version is greater than or equal to 20140609.
        IntDoublePair[] StarRatingTaiko { get; set; }
        IntDoublePair[] StarRatingCTB { get; set; }
        IntDoublePair[] StarRatingMania { get; set; }
        int DrainTime { get; set; }
        int TotalTime { get; set; }
        int AudioPreviewTime { get; set; }
        TimingPoint[] TimingPoints { get; set; } 
        int DifficultyID { get; set; }
        int BeatmapID { get; set; }
        int ThreadID { get; set; }
        byte GradeStandard { get; set; }
        byte GradeTaiko { get; set; }
        byte GradeCTB { get; set; }
        byte GradeMania { get; set; }
        short LocalBeatmapOffset { get; set; }
        float StackLeniency { get; set; }
        PlayMode GameplayMode { get; set; }
        string SongSource { get; set; }
        string SongTags { get; set; }
        short OnlineOffset { get; set; }
        string TitleFont { get; set; }
        bool IsUnplayed { get; set; }
        long LastPlayedTime { get; set; }
        bool IsOsz2 { get; set; }
        string FolderName { get; set; }
        long LastCheckedTime { get; set; }
        bool IgnoreBeatmapSound { get; set; }
        bool IgnoreBeatmapSkin { get; set; }
        bool DisableStoryboard { get; set; }
        bool DisableVideo { get; set; }
        bool VisualOverride { get; set; }
        short? Unknown { get; set; } // Unknown. Only present if version is less than 20140609.
        int LastModificationTimeInt { get; set; }
        byte ManiaScrollSpeed { get; set; }
    }
}
