using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;
using static System.Formats.Asn1.AsnWriter;

namespace OsuDatabaseControl.IO.Readers
{
    public class ScoresDBReader
    {
        static public Scores ReadScores(string filename)
        {
            Scores scores = new Scores();
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    OsuBinaryReader reader = new OsuBinaryReader(stream);
                    int Version = reader.ReadInt32();
                    int NumberOfBeatmaps = reader.ReadInt32();
                    while (stream.Position < stream.Length)
                    {
                        string mapHash = reader.ReadString();
                        int scoresCount = reader.ReadInt32();

                        for (int i = 0; i < scoresCount; i++)
                        {
                            scores.AddScore(Score.ReadScore(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while trying to read scores.db file: {ex.Message}");
            }
            return scores;
        }
    }
}
