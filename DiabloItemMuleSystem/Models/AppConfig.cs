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

        public static string GetConString()
        {
            string json = File.ReadAllText("config.json");

            AppConfig config = JsonSerializer.Deserialize<AppConfig>(json);

            return config.ConnectionString;
        }
        public static string GetFilePath()
        {
            string json = File.ReadAllText("config.json");

            AppConfig config = JsonSerializer.Deserialize<AppConfig>(json);

            return config.FilePath; 
        }
    }
}
