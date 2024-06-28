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
                    int Version = reader.ReadInt32();
                    int FolderCount = reader.ReadInt32();
                    bool AccountUnlocked = reader.ReadBoolean();
                    DateTime UnlockDate = reader.ReadDateTime();
                    string PlayerName = reader.ReadString();
                    int beatmapsCount = reader.ReadInt32();
                    for (int i = 0; i < beatmapsCount; i++)
                    {
                        beatmaps.AddBeatmap(Beatmap.ReadBeatmap(reader, null, Version));
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
    }
}
