using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OsuDatabaseControl.Config
{
    [Serializable]
    public class Config
    {
        public string OsuDirectory { get; set; }
        public bool AutoStart { get; set; }

        public Config()
        {
            OsuDirectory = null;
            AutoStart = false;
        }
    }
}
