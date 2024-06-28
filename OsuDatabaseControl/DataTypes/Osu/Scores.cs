using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDatabaseControl.DataTypes.Osu
{
    public class Scores
    {
        private List<Score> scores;

        public Scores() {
            scores = new List<Score>();
        }

        public void AddScore(Score score)
        {
            scores.Add(score);
        }

        public void RemoveScore(Score score)
        {
            scores.Remove(score);
        }

        public List<Score> GetScores()
        {
            return scores;
        }
    }
}
