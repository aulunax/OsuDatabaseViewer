using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDatabaseControl.DataTypes.Common
{
    public class IntDoublePair
    {
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
        public IntDoublePair(int IntValue, double DoubleValue) {
            this.IntValue = IntValue;
            this.DoubleValue = DoubleValue;
        }
    }
}
