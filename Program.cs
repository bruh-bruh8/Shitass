using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Text;
using System.Linq;

namespace shitass
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("loading...");
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
                string[] settings = { "wintitle=shitass", "bgcolor=Black", "fgcolor=White" };
                System.IO.File.WriteAllLines(SettingsPath, settings, Encoding.UTF8);

            }
            string WinTitle = System.IO.File.ReadLines(SettingsPath).First().Split('=')[1];
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(1).First().Split('=')[1], true);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(2).First().Split('=')[1], true);
            Thread winThread = new Thread(() => RefreshWinTitle(WinTitle));
            winThread.Start();
            Console.Clear();
            Console.WriteLine("Shitass\n");
            while (true)
            {

                Console.Write(user + "> ");

                string cmd;
                cmd = Console.ReadLine();

                string[] args = cmd.ToLower().Split(' ');

                switch (args[0])
                {
                    case "cmd":
                        Console.WriteLine(
                            "   _____ __    _ __                     _  __\n" +
                            "  / ___// /_  (_) /_____ ___________   | |/ /\n" +
                            "  \\__ \\/ __ \\/ / __/ __ `/ ___/ ___/   |   / \n" +
                            " ___/ / / / / / /_/ /_/ (__  |__  )   /   |  \n" +
                            "/____/_/ /_/_/\\__/\\__,_/____/____/   /_/|_|  \n" +
                            "                                             \n" +
                            "Shitass b250217, " +
                            "made by orange\n");
                        break;
                    case "shitass":
                        Console.WriteLine(
                            "   _____ __    _ __                     _  __\n" +
                            "  / ___// /_  (_) /_____ ___________   | |/ /\n" +
                            "  \\__ \\/ __ \\/ / __/ __ `/ ___/ ___/   |   / \n" +
                            " ___/ / / / / / /_/ /_/ (__  |__  )   /   |  \n" +
                            "/____/_/ /_/_/\\__/\\__,_/____/____/   /_/|_|  \n" +
                            "                                             \n" +
                            "Shitass X b9.0, " +
                            "made by orange\n");
                        break;
                    case "help":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine(
                                 "\n" +
                            "cmd - Info about the console\n" +
                            "echo - Outputs text to the console" +
                            "help - Type \"help <command>\" for a more detailed explanation on any command\n" +
                            "driveinfo - Info on drives and removable media\n" +
                            "sysinfo - Info on your PC\n" +
                            "title - Changes window title\n" +
                            "exit - Exits the console\n" +
                            "del - Deletes a file\n" +
                            "ping - Pings an ip or website\n" +
                            "shorten - Shortens a url\n" +
                            "coin - Flips a coin\n" +
                            "cl - Clears the console" +
                            "color - Changes color of console elements"
                                );
                        }
                        else
                        {
                            switch (args[1])
                            {
                                case "cmd":
                                    Console.WriteLine("cmd\nShows info on the console\n");
                                    break;

                                case "echo":
                                    Console.WriteLine("echo\nOutputs text to the console");
                                    break;

                                case "driveinfo":
                                    Console.WriteLine("driveinfo\nShows info on drives and removable media\n");
                                    break;

                                case "sysinfo":
                                    Console.WriteLine("sysinfo\nShows info on your computer\n");
                                    break;

                                case "title":
                                    Console.WriteLine("title\nChanges the title of the console\n");
                                    break;

                                case "exit":
                                    Console.WriteLine("exit\nExits the console\n");
                                    break;

                                case "del":
                                    Console.WriteLine("del\nDeletes a file (folders not supported)\n");
                                    break;

                                case "ping":
                                    Console.WriteLine("ping\nPings an ip or website and shows info on the request\n");
                                    break;

                                case "shorten":
                                    Console.WriteLine("shorten\nShortens a url\nOptional args: /alt: uses v.gd instead of is.gd\n");
                                    break;

                                case "coin":
                                    Console.WriteLine("coin\nFlips a coin");
                                    break;

                                case "cl":
                                    Console.WriteLine("cl\nClears the screen");
                                    break;

                                case "color":
                                    Console.WriteLine("color\nChanges the color of the console background/text");
                                    break;

                                default:
                                    Console.WriteLine("Not a valid command");
                                    break;
                            }

                        }
                        break;

                    case "echo":
                        string echo = string.Join(" ", args.Skip(1));
                        Console.WriteLine("\n" + echo + "\n");
                        break;
                    case "driveinfo":

                        string drives = "";

                        foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                        {
                            try
                            {
                                double totalSizeGB = DriveInfo1.TotalSize / (1024.0 * 1024.0 * 1024.0);
                                double freeSpaceGB = DriveInfo1.AvailableFreeSpace / (1024.0 * 1024.0 * 1024.0);

                                drives += $"\nDrive: {DriveInfo1.Name}\n [Volume Label: {DriveInfo1.VolumeLabel}\n [Type: {DriveInfo1.DriveType}\n [Format: {DriveInfo1.DriveFormat}\n [Total Size: {totalSizeGB:F2} GB\n [Free Space: {freeSpaceGB:F2} GB\n";
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("something went wrong: " + e.Message);
                            }
                        }

                        Console.WriteLine(drives);
                        break;

                    case "sysinfo":

                        string sysi = "";

                        Console.WriteLine("Getting info...");
                        var OS = Environment.GetEnvironmentVariable("OS");
                        var OSVersion = Environment.OSVersion;
                        bool bit = Environment.Is64BitOperatingSystem;
                        var Letter = Environment.GetEnvironmentVariable("HOMEDRIVE");
                        var SystemDirectory = Environment.SystemDirectory;
                        var CurrentDirectory = Environment.CurrentDirectory;
                        var ProcessorCount = Environment.ProcessorCount;
                        var PCName = Environment.GetEnvironmentVariable("COMPUTERNAME");
                        var ProcessorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                        var ProcessorIdentifier = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                        var ProcessorLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL");
                        var ProcessorRevision = Environment.GetEnvironmentVariable("PROCESSOR_REVISION");
                        var CLRVersion = Environment.Version;

                        try
                        {
                            sysi += $"\nOS: {OS}\n [OS Version: {OSVersion}\n [64-bit: {bit}\n [Main Drive Letter: {Letter}\n [System Directory: {SystemDirectory}\n [Current Directory: {CurrentDirectory}\n [Processor Architecture: {ProcessorArchitecture}\n [Processor Identifier: {ProcessorIdentifier}\n [Processor Level: {ProcessorLevel}\n [Processor Count: {ProcessorCount}\n [Processor Revision: {ProcessorRevision}\n [Computer Name: {PCName}\n [Username: {user}\n [CLR Version: {CLRVersion}\n";
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Something went wrong: " + e.Message);
                        }
                        Console.WriteLine(sysi);
                        break;

                    case "del":

                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: del <path>");
                            break;
                        }

                        string DelPath = args[1];

                        if (System.IO.File.Exists(DelPath) || Directory.Exists(DelPath))
                        {
                            try
                            {
                                System.IO.File.Delete(DelPath);
                                Console.WriteLine($"successfully deleted {DelPath}");
                                break;
                            }
                            catch
                            {
                                // File.Delete(CurrentDir + "\\" + args[1]);
                                try
                                {
                                    Directory.Delete(DelPath);
                                    Console.WriteLine($"successfully deleted {DelPath}");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Something went wrong: " + e.Message);
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("file doesnt exist bruh..");
                        }

                        break;

                    case "ping":

                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: ping <ip or website>");
                            break;
                        }
                        ping(args[1]);
                        break;

                    case "title":

                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: title <new window title> OR title -reset");
                            break;
                        }
                        if (args[1] == "-reset")
                        {
                            System.IO.File.WriteAllText(SettingsPath, "wintitle=shitass", Encoding.UTF8);
                            Console.WriteLine("Reset window title, restart for your changes to take effect");
                            break;
                        }
                        else
                        {
                            string wintitle;

                            wintitle = string.Join(" ", args.Skip(1));

                            if (wintitle == "")
                            {
                                Console.WriteLine("nothing provided");
                                break;
                            }

                            Console.WriteLine($"New window title: {wintitle}\n");
                            string[] lines = File.ReadAllLines(SettingsPath);
                            lines[0] = "wintitle=" + wintitle;
                            File.WriteAllLines(SettingsPath, lines);
                            Console.WriteLine("Restart for your changes to take effect");
                        }
                        break;

                    case "color":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Usage: color background/text <color>");
                            Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                            break;
                        }
                        string property = args[1].ToLower();
                        string color = args[2];
                        try
                        {
                            string[] lines = File.ReadAllLines(SettingsPath);
                            switch (property)
                            {
                                case "foreground":
                                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    Console.WriteLine($"Changed foreground color to {color}");
                                    
                                    lines[2] = "fgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    break;

                                case "fg":
                                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    Console.WriteLine($"Changed foreground color to {color}");
                                    lines[2] = "fgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    break;

                                case "text":
                                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    Console.WriteLine($"Changed foreground color to {color}");
                                    lines[2] = "fgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    break;

                                case "background":
                                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    lines[1] = "bgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    Console.Clear();
                                    Console.WriteLine("shitass\n");
                                    Console.WriteLine($"Changed background color to {color}\n");
                                    break;

                                case "bg":
                                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    lines[1] = "bgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    Console.Clear();
                                    Console.WriteLine("shitass\n");
                                    Console.WriteLine($"Changed background color to {color}\n");
                                    break;

                                case "-reset":
                                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "black", true);
                                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "white", true);
                                    lines[1] = "bgcolor=black";
                                    lines[1] = "fgcolor=white";
                                    File.WriteAllLines(SettingsPath, lines);
                                    Console.WriteLine("shitass\n");
                                    Console.WriteLine("Reset console colors");
                                    break;

                                default:
                                    Console.WriteLine("Invalid property, use 'text' or 'background'.");
                                    break;
                            }
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Error: Invalid color");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Something went wrong: " + e.Message);
                        }
                        break;

                    case "shorten":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: shorten <url>");
                            break;
                        }
                        if (args.Length >= 3)
                        {
                            if (args[2] == "-alt")
                            {
                                await Shorten(args[1], true);
                                break;
                            }
                        }
                        else
                        {

                            try
                            {
                                await Shorten(args[1], false);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;

                    case "coin":
                        switch (new Random().Next(0, 2))
                        {
                            case 0:
                                Console.WriteLine("Heads");
                                break;
                            case 1:
                                Console.WriteLine("Tails");
                                break;
                        }
                        break;

                    case "":
                        Console.WriteLine("");
                        break;

                    case "cl":
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("wtf you saying bruh..");
                        break;
                }
            }
        }
        public static void ping(string host)
        {
            Ping p = new Ping();
            PingReply r;
            if ((!(host == "localhost") && !host.Contains(".") && !host.Contains(":")) || host.Length < 7 || host.Length > 39)
            {
                Console.WriteLine("Not an IP or URL.");
            }
            else
            {
                try
                {
                    if (host.ToLower() == "localhost")
                    {
                        r = p.Send("127.0.0.1");
                    }
                    else
                    {
                        r = p.Send(host);
                    }
                    if (r.Status == IPStatus.Success)
                    {
                        Console.WriteLine("Ping to " + host.ToString() + "\n[" + r.Address.ToString() + "]\n" + "Status: Successful\n"
                           + "Response delay: " + r.RoundtripTime.ToString() + " ms\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"failed to ping {host}\nError: " + e.Message);
                }
            }
        }
        public static async Task<string> Shorten(string url, bool alt)
        {
            var client = new HttpClient();
            string uri;
            if (alt == true)
            {
                uri = Uri.EscapeUriString($"https://v.gd/create.php?format=simple&url={url}&logstats=1");
            }
            else
            {
                uri = Uri.EscapeUriString($"https://is.gd/create.php?format=simple&url={url}&logstats=1");
            }
            var responseMessage = await client.GetAsync(uri);
            var response = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(response);
            }
            Console.WriteLine("\n" + response + "\n");
            return response;
        }
        public static void RefreshWinTitle(string t)
        {
            while (true)
            {
                var Time = (DateTime.Now);
                Console.Title = t + " | " + Time;
                Thread.Sleep(5);
            }
        }
    }
}
