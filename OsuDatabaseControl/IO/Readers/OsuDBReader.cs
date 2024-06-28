using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;

namespace OsuDatabaseControl.IO.Readers
{
    public class OsuDBReader
    {
        static public Beatmaps ReadBeatmaps(string filename)
        {
            Beatmaps beatmaps = new Beatmaps();
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    OsuBinaryReader reader = new OsuBinaryReader(stream);
                    OsuDBInfo osuInfo = new OsuDBInfo();
                    OsuDBInfo.ReadOsuDBInfo(reader, osuInfo);

                    for (int i = 0; i < osuInfo.BeatmapsCount; i++)
                    {
                        beatmaps.AddBeatmap(Beatmap.ReadBeatmap(reader, null, osuInfo.Version));
                    }
                    UserPermissions userPermissions = (UserPermissions)reader.ReadInt32();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while trying to read osu!.db file: {ex.Message}");
            }
            return beatmaps;
        }

        static public OsuDBInfo ReadOsuDBInfo(string filename)
        {
            OsuDBInfo osuInfo = new OsuDBInfo();
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    OsuBinaryReader reader = new OsuBinaryReader(stream);
                    OsuDBInfo.ReadOsuDBInfo(reader, osuInfo);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while trying to read OsuDBInfo from osu!.db file: {ex.Message}");
            }
            return osuInfo;
        }
    }
}
