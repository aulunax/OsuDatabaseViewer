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
using OsuFormatReader.Sections;
using OsuFormatReader.Sections.EventTypes;
using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using static System.Formats.Asn1.AsnWriter;

namespace OsuDatabaseControl // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Events events = new Events();
            
            events.AddEvent(new BackgroundsEvent(0, new BackgroundsEventParams("filename",1,2)));
            events.AddEvent(new VideosEvent(0, new VideosEventParams("filenamevideo",3,4)));

            
            IEvent? e = events.GetEvent(1);
            
            VideosEvent ve = (VideosEvent)e;
            Console.WriteLine(ve.eventType);
            
            //General.Read(new OsuFormatReader.OsuFormatReader(new StreamReader(new FileStream("osu!.db", FileMode.Open))));
            

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