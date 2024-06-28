using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataTypes.Osu;

namespace OsuDatabaseControl.DataTypes.Osu.OsuStd
{
    public class OsuScore : Score
    {
        float _aimValue;
        float _speedValue;
        float _accuracyValue;
        float _flashlightValue;
        float _effectiveMissCount;

        float _totalValue;

        public float TotalValue()
        {
            return _totalValue;
        }

    }
}
