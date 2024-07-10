using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDatabaseControl.DataTypes.Common
{
    public class TimingPoint
    {
        public double BPM { get; set; }
        public double Offset { get; set; }
        public bool Uninherited { get; set; }

        public TimingPoint(double BPM, double Offset, bool Uninherited)
        {
            this.BPM = BPM;
            this.Offset = Offset;
            this.Uninherited = Uninherited;
        }
        
    }
}