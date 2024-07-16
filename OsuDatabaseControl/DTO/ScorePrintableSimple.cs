using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Interfaces;

namespace OsuDatabaseControl.DTO
{
    public class ScorePrintableSimple : ICloneable
    {
        public PlayMode PlayMode { get; set; }
        public string MapHash { get; set; }
        public string PlayerName { get; set; }
        public short C300 { get; set; }
        public short C100 { get; set; }
        public short C50 { get; set; }
        public short Miss { get; set; }
        public int TotalScore { get; set; }
        public short MaxCombo { get; set; }
        public bool Perfect { get; set; }
        public Mods Mods { get; set; }
        public double AdditionalMods { get; set; }
        public DateTime Date { get; set; }

        public ScorePrintableSimple(Score score)
        {
            PlayMode = score.PlayMode;
            MapHash = score.MapHash;
            PlayerName = score.PlayerName;
            C300 = score.C300;
            C100 = score.C100;
            C50 = score.C50;
            Miss = score.Miss;
            TotalScore = score.TotalScore;
            MaxCombo = score.MaxCombo;
            Perfect = score.Perfect;
            Mods = score.Mods;
            AdditionalMods = score.AdditionalMods;
            Date = score.Date;
        }

        public override string ToString()
        {
            return $"{PlayerName} on {((Mods)Mods).ToAcronyms()}{MapHash} in {PlayMode}\n" +
                $"Played on {Date}\n" +
                $"Total Score: {TotalScore}\n" +
                $"300s: {C300}\n100s: {C100}\n50s: " +
                $"{C50}\nMiss: {Miss}\nMax Combo: {MaxCombo} " +
                $"{ (Perfect ? "(PFC)" : "") }";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
