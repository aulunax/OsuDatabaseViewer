using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataTypes.Common;

namespace OsuDatabaseControl.IO.Readers
{
    public class OsuBinaryReader : BinaryReader
    {
        public OsuBinaryReader([NotNull] Stream input) : base(input)
        {
        }

        public OsuBinaryReader([NotNull] Stream input, [NotNull] Encoding encoding) : base(input, encoding)
        {
        }

        public override string ReadString()
        {
            if (ReadByte() == 11)
                return base.ReadString();
            else
                return "";
        }

        public IntDoublePair ReadIntDoublePair()
        {
            byte IntByte = base.ReadByte();
            int IntValue = base.ReadInt32();
            byte DoubleByte = base.ReadByte();
            double DoubleValue = base.ReadDouble();

            if (IntByte != 0x08 || DoubleByte != 0x0d)
                throw new Exception("Invalid db format when reading IntDobulePair.");

            return new IntDoublePair(IntValue, DoubleValue);
        }

        public TimingPoint ReadTimingPoint()
        {
            double BPM = base.ReadDouble();
            double Offset = base.ReadDouble();
            bool Uninherited = base.ReadBoolean();

            return new TimingPoint(BPM, Offset, Uninherited);
        }

        public DateTime ReadDateTime()
        {
            long ticks = base.ReadInt64();
            return GetDate(ticks);
        }

        public DateTime GetDate(long ticks)
        {
            if (ticks < 0L)
            {
                return new DateTime();
            }
            try
            {
                return new DateTime(ticks, DateTimeKind.Utc);
            }
            catch (Exception e)
            {
                return new DateTime();
            }
        }
    }
}
