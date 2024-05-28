namespace AnonCrypter.Crypter
{
    public class CrypterOptions
    {
        public bool DropFile { get; set; }

        public bool AntiVM { get; set; }

        public bool SingleInstance { get; set; }

        public string FakeError { get; set; }

        public int Delay { get; set; }

        public bool BotKiller { get; set; }

        public string[] BindedFiles { get; set; }

        public bool UACBypass { get; set; }

        public bool WDExclusions { get; set; }

        public bool Startup { get; set; }

        public bool Startup_MeltFile { get; set; }

        public bool Startup_BF { get; set; }

        public bool Startup_FE { get; set; }
    }
}