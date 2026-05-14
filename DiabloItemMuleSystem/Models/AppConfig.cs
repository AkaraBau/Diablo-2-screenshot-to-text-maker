using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace DiabloItemMuleSystem.Models
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public string FilePath { get; set; }

        public static AppConfig GetConfigurationFromJson()
        {
            try
            {
                string json = File.ReadAllText("config.json");

                AppConfig appConfig = JsonSerializer.Deserialize<AppConfig>(json);

                if (appConfig.ConnectionString == null || appConfig.FilePath == null)
                {
                    throw new Exception("Failed to deserialize config.json"); 
                }

                return appConfig;
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Cant find config.json");
            }
        }
    }
}
