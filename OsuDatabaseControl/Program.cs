using System;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using OsuDatabaseControl.IO.Readers;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.DTO;
using OsuFormatReader.Interfaces;
using OsuFormatReader.IO;
using OsuFormatReader.Sections;
using OsuFormatReader.Sections.EventTypes;
using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Tests;
using static System.Formats.Asn1.AsnWriter;

namespace OsuDatabaseControl // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {

            return;
            CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;


            Scores scores = ScoresDBReader.ReadScores("scores.db");
            Beatmaps beatmaps = OsuDBReader.ReadBeatmaps("osu!.db");
            BeatmapDictionary beatmapDictionary = new BeatmapDictionary(beatmaps);

            List<Score> sc = scores.GetScores().OrderBy(s => s.Date).ToList();
            using (StreamWriter writer = new StreamWriter("scores.txt"))
            {
                foreach (Score score in sc)
                {
                    writer.Write(new ScorePrintableSimple(score) + "\n\n");
                }
            }
            Console.WriteLine($"Content written to file scores.txt");


            List<Beatmap> bm = beatmaps.GetBeatmaps();
            using (StreamWriter writer = new StreamWriter("beatmaps.txt"))
            {
                foreach (Beatmap beatmap in bm)
                {
                    writer.Write(new BeatmapPrintable(beatmap) + "\n\n");
                }
            }
            Console.WriteLine($"Content written to file beatmaps.txt");


            using (StreamWriter writer = new StreamWriter("ProperScores.txt"))
            {
                foreach (Score score in sc)
                {
                    writer.Write(new FullScore(score, beatmapDictionary) + "\n\n");
                }
            }
            Console.WriteLine($"Content written to file ProperScores.txt");

        }


    }
}