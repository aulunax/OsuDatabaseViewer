using OsuDatabaseControl.Interfaces;
using OsuDatabaseControl.IO.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDatabaseControl.DataTypes.Osu
{
    public class OsuDBInfo
    {
        public int Version { get; set; }
        public int FolderCount { get; set; }
        public bool AccountUnlocked { get; set; }
        public DateTime UnlockDate { get; set; }
        public string PlayerName { get; set; }
        public int BeatmapsCount { get; set; }

        public static OsuDBInfo ReadOsuDBInfo(OsuBinaryReader reader, OsuDBInfo outobj = null)
        {
            if (outobj == null)
                outobj = new OsuDBInfo();

            outobj.Version = reader.ReadInt32();
            outobj.FolderCount = reader.ReadInt32();
            outobj.AccountUnlocked = reader.ReadBoolean();
            outobj.UnlockDate = reader.ReadDateTime();
            outobj.PlayerName = reader.ReadString();
            outobj.BeatmapsCount = reader.ReadInt32();

            return outobj;
        }
    }
}
