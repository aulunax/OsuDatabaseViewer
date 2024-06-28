using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataTypes.Osu
{
    public class Beatmaps
    {
        private List<Beatmap> beatmaps;

        public Beatmaps()
        {
            beatmaps = new List<Beatmap>();
        }

        public void AddBeatmap(Beatmap beatmap)
        {
            beatmaps.Add(beatmap);
        }

        public void RemoveBeatmap(Beatmap beatmap)
        {
            beatmaps.Remove(beatmap);
        }

        public List<Beatmap> GetBeatmaps()
        {
            return beatmaps;
        }
    }
}
