using AnonCrypter.Crypter;
using Newtonsoft.Json;
using System.IO;

namespace AnonCrypter.Settings
{
    internal class Settings
    {
        private static string _saveDirectory = string.Empty;
        private static string _savePath = string.Empty;

        public static string SaveDirectory
        {
            get => Settings._saveDirectory;
            set
            {
                Settings._saveDirectory = value;
                Settings._savePath = Settings._saveDirectory + "\\settings.json";
            }
        }

        public static Settings Load() => !File.Exists(Settings._savePath) ? (Settings)null : JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Settings._savePath));

        public static void Save(Settings obj) => File.WriteAllText(Settings._savePath, JsonConvert.SerializeObject((object)obj, (Formatting)1));

        public string InputFile { get; set; }

        public bool PumpFile { get; set; }

        public int PumpFileAmount { get; set; }

        public CrypterOptions CrypterOptions { get; set; }
    }
}
