using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Text;
using System.Linq;

namespace shitass
{
    class Program
    {
        public const string version = "251005"; // current build (remember to update lol)
        private static string currentTitle = "shitass";
        private static readonly object titleLock = new object();

        public static async Task<string> GetLatestVersion()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    string response = await client.GetStringAsync("https://bruh-bruh8.github.io/version.txt");
                    return response.Trim();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        static async Task Main()
        {

            string user = Environment.UserName;
            string SettingsPath = $@"C:\Users\{user}\AppData\Roaming\Shitass\settings.txt";
            var settings = Settings.Load(SettingsPath);

            // Only check for updates if enabled
            if (settings.AutoCheckUpdates)
            {
                string latestVersion = await GetLatestVersion();
                if (latestVersion != null && latestVersion != version)
                {
                    Console.WriteLine($"Update available! Latest: b{latestVersion} (You have: b{version})");
                    Console.Write("Download now? (y/n): ");

                    string response = Console.ReadLine()?.ToLower();
                    if (response == "y" || response == "yes")
                    {
                        string downloadPath = Path.Combine(
                            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                            $"shitass-{latestVersion}.exe"
                        );

                        await DownloadLatestVersion(downloadPath);
                        Console.WriteLine("\nPress any key to continue with current version...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Download at: https://github.com/bruh-bruh8/Shitass \n");
                        Thread.Sleep(2000);
                    }
                }
            }

            if (settings.Version != version)
            {
                Console.WriteLine($"You updated to Shitass b{version} from b{settings.Version}.\nThe changelog can be found at https://github.com/bruh-bruh8/Shitass/blob/main/changelog.md");
                settings.Version = version;
                settings.Save(SettingsPath);
                Thread.Sleep(3000);
            }

            var context = new CommandContext
            {
                User = user,
                SettingsPath = SettingsPath,
                Settings = settings
            };

            var registry = new CommandRegistry();
            registry.AutoRegisterCommands();

            var helpCmd = new HelpCommand(registry);
            registry.RegisterCommand(helpCmd);

            lock (titleLock)
            {
                currentTitle = settings.WindowTitle;
            }

            Thread title = new Thread(() => RefreshWinTitle(settings));
            title.IsBackground = true;
            title.Start();

            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settings.BackgroundColor, true);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settings.ForegroundColor, true);

            Console.Clear();
            if (settings.ShowStartupMessage)
            {
                Console.WriteLine("shitass\n");
            }

            while (true)
            {
                string promptName = string.IsNullOrWhiteSpace(settings.PromptName)
                    ? user
                    : settings.PromptName;

                Console.Write($"{promptName}{settings.PromptSymbol} ");

                string cmd;
                cmd = Console.ReadLine();

                string[] args = cmd.Split(' ');

                var command = registry.GetCommand(args[0]);
                if (command != null)
                {
                    try
                    {
                        await command.ExecuteAsync(args, context);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
                else if (!string.IsNullOrWhiteSpace(args[0]))
                {
                    Console.WriteLine("wtf you saying bruh..");
                }
            }
        }
        public static void RefreshWinTitle(Settings settings)
        {
            while (true)
            {
                string titleToDisplay;
                lock (titleLock)
                {
                    titleToDisplay = currentTitle;
                }

                if (settings.ShowTimeInTitle)
                {
                    string dateTime = "";
                    switch (settings.TitleDateFormat.ToLower())
                    {
                        case "time":
                            dateTime = DateTime.Now.ToString("HH:mm:ss");
                            break;
                        case "date":
                            dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                            break;
                        case "short":
                            dateTime = DateTime.Now.ToString("HH:mm");
                            break;
                        default:  // full
                            dateTime = DateTime.Now.ToString();
                            break;
                    }
                    Console.Title = titleToDisplay + " | " + dateTime;
                }
                else
                {
                    Console.Title = titleToDisplay;
                }

                Thread.Sleep(50);
            }
        }
        public static void UpdateTitle(string newTitle)
        {
            lock (titleLock)
            {
                currentTitle = newTitle;
            }
        }
        public static async Task DownloadLatestVersion(string downloadPath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Downloading latest version...");

                    
                    string downloadUrl = "https://github.com/bruh-bruh8/Shitass/releases/latest/download/Shitass.exe";

                    var response = await client.GetAsync(downloadUrl);
                    response.EnsureSuccessStatusCode();

                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(downloadPath, fileBytes);

                    Console.WriteLine($"Downloaded to: {downloadPath}");
                    Console.WriteLine("The new version will start when you close this window.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Download failed: {e.Message}");
                Console.WriteLine("You can manually download from: https://github.com/bruh-bruh8/Shitass");
            }
        }
    }
}