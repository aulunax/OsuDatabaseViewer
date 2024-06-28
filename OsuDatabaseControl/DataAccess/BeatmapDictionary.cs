using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataTypes.Osu;
using Test.DTO;
using Test.Interfaces;

namespace Test.DataAccess
{
    public class BeatmapDictionary
    {
        Dictionary<string, Beatmap> beatmapDict;
        public BeatmapDictionary(Beatmaps beatmaps) {
            beatmapDict = beatmaps.GetBeatmaps().ToDictionary(b => b.MD5Hash);
        }

        public Beatmap GetBeatmap(string hash)
        {
            return beatmapDict[hash];
        }
    }
}
