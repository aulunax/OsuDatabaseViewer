using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OsuDatabaseControl.Enums.Display;

namespace OsuDatabaseControl.Config
{
    [Serializable]
    public class Config
    {
        public string OsuDirectory { get; set; }
        public string Username { get; set; }
        public bool AutoStart { get; set; }
        
        public bool IsSideScoreInfoShown { get; set; }
        public MainColumnVisibility MainColumnVisibility { get; set; }

        public Config()
        {
            OsuDirectory = null;
            Username = null;
            AutoStart = false;
            MainColumnVisibility = MainColumnVisibility.Default;
            IsSideScoreInfoShown = true;
        }
    }
}
