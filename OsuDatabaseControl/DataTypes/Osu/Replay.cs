﻿using OsuDatabaseControl.IO.Readers;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Interfaces;

namespace OsuDatabaseControl.DataTypes.Osu
{
    public class Replay : IReplay, ICloneable
    {
        public PlayMode PlayMode { get; set; }
        public int Version { get; set; }
        public string MapHash { get; set; }
        public string PlayerName { get; set; }
        public string ReplayHash { get; set; }
        public short C300 { get; set; }
        public short C100 { get; set; }
        public short C50 { get; set; }
        public short Geki { get; set; }
        public short Katu { get; set; }
        public short Miss { get; set; }
        public int TotalScore { get; set; }
        public short MaxCombo { get; set; }
        public bool Perfect { get; set; }
        public Mods Mods { get; set; }
        public double AdditionalMods { get; set; }
        public virtual string ReplayData { get; set; }
        public DateTime Date { get; set; }
        public long DateTicks { get; set; }
        public int CompressedReplayLength { get; set; }
        public byte[] CompressedReplay { get; set; }
        public long OnlineScoreId { get; set; } = -1;

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
                    case PlayMode.Mania:
                        return ((C300 + Geki) * 300.0 + Katu * 200.0 + C100 * 100.0 + C50 * 50.0) /
                               ((C300 + Geki + Katu + C100 + C50 + Miss) * 300.0);
                    case PlayMode.CatchTheBeat:
                        return (C300 + C100 + C50) / (double)(C300 + C100 + C50 + Katu + Miss);
                    default:
                        return 0;
                }
            }
        }


        public static IReplay Read(OsuBinaryReader reader, IReplay outobj = null, bool minimalLoad = true, int? version = null)
        {
            if (outobj == null)
                outobj = new Replay();
            outobj.PlayMode = (PlayMode)reader.ReadByte();
            outobj.Version = reader.ReadInt32();
            outobj.MapHash = reader.ReadString();
            outobj.PlayerName = reader.ReadString();
            outobj.ReplayHash = reader.ReadString();
            outobj.C300 = reader.ReadInt16();
            outobj.C100 = reader.ReadInt16();
            outobj.C50 = reader.ReadInt16();
            outobj.Geki = reader.ReadInt16();
            outobj.Katu = reader.ReadInt16();
            outobj.Miss = reader.ReadInt16();
            outobj.TotalScore = reader.ReadInt32();
            outobj.MaxCombo = reader.ReadInt16();
            outobj.Perfect = reader.ReadBoolean();
            outobj.Mods = (Mods)reader.ReadInt32();
            outobj.ReplayData = reader.ReadString();
            outobj.DateTicks = reader.ReadInt64();
            outobj.Date = reader.GetDate(outobj.DateTicks);
            outobj.CompressedReplayLength = reader.ReadInt32();
            if (outobj.CompressedReplayLength > 0)
            {
                if (minimalLoad)
                    reader.ReadBytes(outobj.CompressedReplayLength);
                else
                    outobj.CompressedReplay = reader.ReadBytes(outobj.CompressedReplayLength);
            }

            version = version ?? outobj.Version;
            if (version >= 20140721)
                outobj.OnlineScoreId = reader.ReadInt64();
            else if (version >= 20121008)
                outobj.OnlineScoreId = reader.ReadInt32();

            if ((((Mods)outobj.Mods) & Osu.Mods.Tp) != 0)
                outobj.AdditionalMods = reader.ReadDouble();

            return outobj;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
