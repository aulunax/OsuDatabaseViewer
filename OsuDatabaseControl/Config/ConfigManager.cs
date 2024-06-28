using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OsuDatabaseControl.Config
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private readonly string _configFilePath = "config.xml";
        private Config _config;

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }
                return _instance;
            }
        }

        public Config Config
        {
            get { return _config; }
            set { _config = value; }
        }

        private ConfigManager()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream(_configFilePath, FileMode.OpenOrCreate))
                {
                    _config = (Config)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                _config = new Config();
            }
        }

        public void SaveConfig()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream(_configFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, _config);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
