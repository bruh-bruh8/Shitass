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
        public const string version = "251004"; // current build (remember to update lol)
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
            string latestVersion = await GetLatestVersion();
            if (latestVersion != null && latestVersion != version)
            {
                Console.WriteLine($"Update available! Latest: b{latestVersion} (You have: b{version})");
                Console.WriteLine("Download at: https://github.com/bruh-bruh8/Shitass \n");
                Thread.Sleep(3000);
            }
            donutUtil donut = new donutUtil();

            string user = Environment.UserName;
            string SettingsPath = $@"C:\Users\{user}\AppData\Roaming\Shitass\settings.txt";
            string FilePath = $@"C:\Users\{user}\AppData\Roaming\Shitass\";
            if (!System.IO.File.Exists(SettingsPath))
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                var settingfile = System.IO.File.Create(SettingsPath);
                settingfile.Close();
                string[] settings = { "version=" + version, "wintitle=shitass", "bgcolor=Black", "fgcolor=White" };
                System.IO.File.WriteAllLines(SettingsPath, settings, Encoding.UTF8);
                Console.WriteLine($"Created settings file at {SettingsPath}");
                Thread.Sleep(3000);
            }
            if (System.IO.File.ReadLines(SettingsPath).First().Split('=')[1] != version)
            {
                File.Delete(SettingsPath);
                var settingfile = System.IO.File.Create(SettingsPath);
                settingfile.Close();
                string[] settings = { "version=" + version, "wintitle=shitass", "bgcolor=Black", "fgcolor=White" };
                System.IO.File.WriteAllLines(SettingsPath, settings, Encoding.UTF8);
                Console.WriteLine($"Old version detected, your settings may have been reset.");
                Thread.Sleep(3000);
            }
            var registry = new CommandRegistry();
            registry.AutoRegisterCommands();

            var helpCmd = new HelpCommand(registry);
            registry.RegisterCommand(helpCmd);

            var context = new CommandContext
            {
                User = user,
                SettingsPath = SettingsPath
            };
            string WinTitle = System.IO.File.ReadLines(SettingsPath).Skip(1).First().Split('=')[1];

            lock (titleLock)
            {
                currentTitle = WinTitle;
            }

            Thread title = new Thread(() => RefreshWinTitle());
            title.IsBackground = true;
            title.Start();

            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(2).First().Split('=')[1], true);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(3).First().Split('=')[1], true);

            Console.Clear();
            Console.WriteLine("shitass\n");
            while (true)
            {

                Console.Write(user + "> ");

                string cmd;
                cmd = Console.ReadLine();

                string[] args = cmd.ToLower().Split(' ');

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
        public static void RefreshWinTitle()
        {
            while (true)
            {
                string titleToDisplay;
                lock (titleLock)
                {
                    titleToDisplay = currentTitle;
                }
                Console.Title = titleToDisplay + " | " + DateTime.Now;
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
    }
}