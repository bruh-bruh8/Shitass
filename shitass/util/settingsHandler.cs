using System.IO;

namespace shitass
{
    public class Settings
    {
        public string Version { get; set; }
        public string WindowTitle { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string PromptName { get; set; }
        public string PromptSymbol { get; set; }
        public bool ShowTimeInTitle { get; set; }
        public string TitleDateFormat { get; set; }
        public bool ShowStartupMessage { get; set; }
        public bool AutoCheckUpdates { get; set; }

        public static Settings Load(string path)
        {
            var settings = new Settings
            {
                Version = "251004",
                WindowTitle = "shitass",
                BackgroundColor = "Black",
                ForegroundColor = "White",
                PromptName = "",  // empty = username
                PromptSymbol = ">",
                ShowTimeInTitle = true,
                TitleDateFormat = "full",  // full, time, date, short
                ShowStartupMessage = true,
                AutoCheckUpdates = true
            };

            if (!File.Exists(path))
            {
                settings.Save(path);
                return settings;
            }

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length != 2) continue;

                var key = parts[0].Trim().ToLower();
                var value = parts[1].Trim();

                switch (key)
                {
                    case "version": settings.Version = value; break;
                    case "wintitle": settings.WindowTitle = value; break;
                    case "bgcolor": settings.BackgroundColor = value; break;
                    case "fgcolor": settings.ForegroundColor = value; break;
                    case "promptname": settings.PromptName = value; break;
                    case "promptsymbol": settings.PromptSymbol = value; break;
                    case "showtimeintitle": settings.ShowTimeInTitle = value.ToLower() == "true"; break;
                    case "titledateformat": settings.TitleDateFormat = value; break;
                    case "showstartupmessage": settings.ShowStartupMessage = value.ToLower() == "true"; break;
                    case "autocheckupdates": settings.AutoCheckUpdates = value.ToLower() == "true"; break;
                }
            }

            return settings;
        }

        public void Save(string path)
        {
            var lines = new[]
            {
                $"version={Version}",
                $"wintitle={WindowTitle}",
                $"bgcolor={BackgroundColor}",
                $"fgcolor={ForegroundColor}",
                $"promptname={PromptName}",
                $"promptsymbol={PromptSymbol}",
                $"showtimeintitle={ShowTimeInTitle}",
                $"titledateformat={TitleDateFormat}",
                $"showstartupmessage={ShowStartupMessage}",
                $"autocheckupdates={AutoCheckUpdates}"
            };

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, lines);
        }

        public void Set(string key, string value, string path)
        {
            switch (key.ToLower())
            {
                case "wintitle": WindowTitle = value; break;
                case "bgcolor": BackgroundColor = value; break;
                case "fgcolor": ForegroundColor = value; break;
                case "promptname": PromptName = value; break;
                case "promptsymbol": PromptSymbol = value; break;
                case "showtimeintitle": ShowTimeInTitle = value.ToLower() == "true"; break;
                case "titledateformat": TitleDateFormat = value; break;
                case "showstartupmessage": ShowStartupMessage = value.ToLower() == "true"; break;
                case "autocheckupdates": AutoCheckUpdates = value.ToLower() == "true"; break;
            }
            Save(path);
        }
    }
}