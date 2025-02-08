using OsuDatabaseControl.IO.Readers;
using OsuDatabaseControl.DataTypes.Common;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Interfaces;

namespace OsuDatabaseControl.DataTypes.Osu
{
    public class Beatmap : IBeatmap, ICloneable
    {
        public int? SizeInBytes { get; set; }
        public string ArtistName { get; set; }
        public string ArtistNameUnicode { get; set; }
        public string SongTitle { get; set; }
        public string SongTitleUnicode { get; set; }
        public string CreatorName { get; set; }
        public string Difficulty { get; set; }
        public string AudioFileName { get; set; }
        public string MD5Hash { get; set; }
        public string OsuFileName { get; set; }
        public BeatmapRankedStatus RankedStatus { get; set; }
        public short NumberOfHitCircles { get; set; }
        public short NumberOfSliders { get; set; }
        public short NumberOfSpinners { get; set; }
        public short NumberOfObjects => (short)(NumberOfSliders + NumberOfHitCircles + NumberOfSpinners);
        public long LastModificationTime { get; set; }
        public float ApproachRate { get; set; }
        public float CircleSize { get; set; }
        public float HPDrain { get; set; }
        public float OverallDifficulty { get; set; }
        public double SliderVelocity { get; set; }
        public IntDoublePair[] StarRatingStandard { get; set; } 
        public IntDoublePair[] StarRatingTaiko { get; set; }
        public IntDoublePair[] StarRatingCTB { get; set; }
        public IntDoublePair[] StarRatingMania { get; set; }
        public int DrainTime { get; set; }
        public int TotalTime { get; set; }
        public int AudioPreviewTime { get; set; }
        public TimingPoint[] TimingPoints { get; set; }
        public int DifficultyID { get; set; }
        public int BeatmapID { get; set; }
        public int ThreadID { get; set; }
        public byte GradeStandard { get; set; }
        public byte GradeTaiko { get; set; }
        public byte GradeCTB { get; set; }
        public byte GradeMania { get; set; }
        public short LocalBeatmapOffset { get; set; }
        public float StackLeniency { get; set; }
        public PlayMode GameplayMode { get; set; }
        public string SongSource { get; set; }
        public string SongTags { get; set; }
        public short OnlineOffset { get; set; }
        public string TitleFont { get; set; }
        public bool IsUnplayed { get; set; }
        public long LastPlayedTime { get; set; }
        public bool IsOsz2 { get; set; }
        public string FolderName { get; set; }
        public long LastCheckedTime { get; set; }
        public bool IgnoreBeatmapSound { get; set; }
        public bool IgnoreBeatmapSkin { get; set; }
        public bool DisableStoryboard { get; set; }
        public bool DisableVideo { get; set; }
        public bool VisualOverride { get; set; }
        public short? Unknown { get; set; } // Unknown. Only present if version is less than 20140609.
        public int LastModificationTimeInt { get; set; }
        public byte ManiaScrollSpeed { get; set; }

        public static Beatmap ReadBeatmap(OsuBinaryReader reader, IBeatmap outobj = null, int? version = null)
        {
            return (Beatmap)Read(reader, outobj, version);
        }

        public static IBeatmap Read(OsuBinaryReader reader, IBeatmap outobj = null, int? version = null)
        {
            if (outobj == null)
                outobj = new Beatmap();

            outobj.SizeInBytes = version < 20191106 ? (int?)reader.ReadInt32() : null;
            outobj.ArtistName = reader.ReadString();
            outobj.ArtistNameUnicode = reader.ReadString();
            outobj.SongTitle = reader.ReadString();
            outobj.SongTitleUnicode = reader.ReadString();
            outobj.CreatorName = reader.ReadString();
            outobj.Difficulty = reader.ReadString();
            outobj.AudioFileName = reader.ReadString();
            outobj.MD5Hash = reader.ReadString();
            outobj.OsuFileName = reader.ReadString();
            outobj.RankedStatus = (BeatmapRankedStatus)reader.ReadByte();
            outobj.NumberOfHitCircles = reader.ReadInt16();
            outobj.NumberOfSliders = reader.ReadInt16();
            outobj.NumberOfSpinners = reader.ReadInt16();
            outobj.LastModificationTime = reader.ReadInt64();

            if (version < 20140609)
            {
                outobj.ApproachRate = reader.ReadByte();
                outobj.CircleSize = reader.ReadByte();
                outobj.HPDrain = reader.ReadByte();
                outobj.OverallDifficulty = reader.ReadByte();
            }
            else
            {
                outobj.ApproachRate = reader.ReadSingle();
                outobj.CircleSize = reader.ReadSingle();
                outobj.HPDrain = reader.ReadSingle();
                outobj.OverallDifficulty = reader.ReadSingle();
            }

            outobj.SliderVelocity = reader.ReadDouble();

            
            if (version >= 20250101)
            {
                int NumberOfStarRatingStandard = reader.ReadInt32();
                IntFloatPair temp;
                outobj.StarRatingStandard = new IntDoublePair[NumberOfStarRatingStandard];
                for (int i = 0; i < NumberOfStarRatingStandard; i++)
                {
                    temp = reader.ReadIntFloatPair();
                    outobj.StarRatingStandard[i] = new IntDoublePair(temp.IntValue, temp.FloatValue);
                }
                int NumberOfStarRatingTaiko = reader.ReadInt32();
                outobj.StarRatingTaiko = new IntDoublePair[NumberOfStarRatingTaiko];
                for (int i = 0; i < NumberOfStarRatingTaiko; i++)
                {
                    temp = reader.ReadIntFloatPair();
                    outobj.StarRatingTaiko[i] = new IntDoublePair(temp.IntValue, temp.FloatValue);
                }
                int NumberOfStarRatingCTB = reader.ReadInt32();
                outobj.StarRatingCTB = new IntDoublePair[NumberOfStarRatingCTB];
                for (int i = 0; i < NumberOfStarRatingCTB; i++)
                {
                    temp = reader.ReadIntFloatPair();
                    outobj.StarRatingCTB[i] = new IntDoublePair(temp.IntValue, temp.FloatValue);
                }
                int NumberOfStarRatingMania = reader.ReadInt32();
                outobj.StarRatingMania = new IntDoublePair[NumberOfStarRatingMania];
                for (int i = 0; i < NumberOfStarRatingMania; i++)
                {
                    temp = reader.ReadIntFloatPair();
                    outobj.StarRatingMania[i] = new IntDoublePair(temp.IntValue, temp.FloatValue);
                }
            }
            else if (version >= 20140609)
            {
                int NumberOfStarRatingStandard = reader.ReadInt32();
                outobj.StarRatingStandard = new IntDoublePair[NumberOfStarRatingStandard];
                for (int i = 0; i < NumberOfStarRatingStandard; i++)
                {
                    outobj.StarRatingStandard[i] = reader.ReadIntDoublePair();
                }
                int NumberOfStarRatingTaiko = reader.ReadInt32();
                outobj.StarRatingTaiko = new IntDoublePair[NumberOfStarRatingTaiko];
                for (int i = 0; i < NumberOfStarRatingTaiko; i++)
                {
                    outobj.StarRatingTaiko[i] = reader.ReadIntDoublePair();
                }
                int NumberOfStarRatingCTB = reader.ReadInt32();
                outobj.StarRatingCTB = new IntDoublePair[NumberOfStarRatingCTB];
                for (int i = 0; i < NumberOfStarRatingCTB; i++)
                {
                    outobj.StarRatingCTB[i] = reader.ReadIntDoublePair();
                }
                int NumberOfStarRatingMania = reader.ReadInt32();
                outobj.StarRatingMania = new IntDoublePair[NumberOfStarRatingMania];
                for (int i = 0; i < NumberOfStarRatingMania; i++)
                {
                    outobj.StarRatingMania[i] = reader.ReadIntDoublePair();
                }
            }
            

            outobj.DrainTime = reader.ReadInt32();
            outobj.TotalTime = reader.ReadInt32();
            outobj.AudioPreviewTime = reader.ReadInt32();

            int NumberOfTimingPoints = reader.ReadInt32();
            outobj.TimingPoints = new TimingPoint[NumberOfTimingPoints];
            for (int i = 0; i < NumberOfTimingPoints; i++)
            {
                outobj.TimingPoints[i] = reader.ReadTimingPoint();
            }

            outobj.DifficultyID = reader.ReadInt32();
            outobj.BeatmapID = reader.ReadInt32();
            outobj.ThreadID = reader.ReadInt32();
            outobj.GradeStandard = reader.ReadByte();
            outobj.GradeTaiko = reader.ReadByte();
            outobj.GradeCTB = reader.ReadByte();
            outobj.GradeMania = reader.ReadByte();
            outobj.LocalBeatmapOffset = reader.ReadInt16();
            outobj.StackLeniency = reader.ReadSingle();
            outobj.GameplayMode = (PlayMode)reader.ReadByte();
            outobj.SongSource = reader.ReadString();
            outobj.SongTags = reader.ReadString();
            outobj.OnlineOffset = reader.ReadInt16();
            outobj.TitleFont = reader.ReadString();
            outobj.IsUnplayed = reader.ReadBoolean();
            outobj.LastPlayedTime = reader.ReadInt64();
            outobj.IsOsz2 = reader.ReadBoolean();
            outobj.FolderName = reader.ReadString();
            outobj.LastCheckedTime = reader.ReadInt64();
            outobj.IgnoreBeatmapSound = reader.ReadBoolean();
            outobj.IgnoreBeatmapSkin = reader.ReadBoolean();
            outobj.DisableStoryboard = reader.ReadBoolean();
            outobj.DisableVideo = reader.ReadBoolean();
            outobj.VisualOverride = reader.ReadBoolean();

            // Read the unknown short if version is less than 20140609
            outobj.Unknown = version < 20140609 ? (short?)reader.ReadInt16() : null;

            outobj.LastModificationTimeInt = reader.ReadInt32();
            outobj.ManiaScrollSpeed = reader.ReadByte();

            return outobj;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
