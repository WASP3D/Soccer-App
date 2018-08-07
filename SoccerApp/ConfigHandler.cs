using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp
{
    public class ConfigHandler
    {
        public static AppSettingsSection AppSettings { get; set; }

        public static void Initilaize()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string xmlFileName = Path.Combine(assemblyFolder, "SoccerApp.config");

            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = xmlFileName;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            var data = config.GetSection("appSettings");
            AppSettings = config.AppSettings;
        }

        public static EngineData Engine {get;set;}
    }
}
