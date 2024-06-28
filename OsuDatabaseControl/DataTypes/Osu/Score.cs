using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.IO.Readers;

namespace OsuDatabaseControl.DataTypes.Osu
{
    public class Score : Replay
    {
        public static Score ReadScore(OsuBinaryReader reader, bool minimalLoad = true, int? version = null)
        {
            return (Score)Read(reader, new Score(), minimalLoad, version);
        }
    }
}
