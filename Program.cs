using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Text;

// shoutout plex https://github.com/plexthedev

/* todo
 * 
 * base64 converter
 * 
 */

namespace oConsole
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = ("Loading...");
            Console.WriteLine("loading...");

            string user = Environment.UserName;
            string SettingsPath = $@"C:\Users\{user}\AppData\Roaming\oConsole\settings.txt";
            string FilePath = $@"C:\Users\{user}\AppData\Roaming\oConsole\";
            string settings = "wintime=1" + Environment.NewLine + "wintitle=oConsole";
            if (!File.Exists(SettingsPath))
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                var oFile = File.Create(SettingsPath);
                oFile.Close();
                File.WriteAllText(SettingsPath, settings, Encoding.UTF8);
                
            }

            // for sysinfo
            var OSVersion = Environment.OSVersion;
            var ProcessorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            var ProcessorIdentifier = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            var ProcessorLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL");
            var SystemDirectory = Environment.SystemDirectory;
            var ProcessorCount = Environment.ProcessorCount;
            var UserDomainName = Environment.UserDomainName;
            var UserName = Environment.UserName;
            var Version = Environment.Version;
            string CurrentDir = $"C:\\Users\\{user}\\";
            Thread WinThread = new Thread(Program.RefreshWinTitle);
            WinThread.Start();
            Console.Clear();

            Console.WriteLine("oConsole\n");
            
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
                            "       _________                            .__          \n" +
                            "  ____ \\_   ___ \\  ____   ____   __________ |  |   ____  \n" +
                            " /  _ \\/    \\  \\/ /  _ \\ /    \\ /  ___/  _ \\|  | _/ __ \\ \n" +
                            "(  <_> )     \\___(  <_> )   |  \\___ (  <_> )  |_\\  ___/ \n" +
                            " \\____/ \\______  /\\____/|___|  /____  >____/|____/\\___  >\n" +
                            "               \\/            \\/     \\/                \\/ \n" +
                            "oConsole b8\n" +
                            "Made by orange");
                        break;
                    case "shitass":
                        Console.WriteLine("rip da og D:");
                        break;
                    case "help":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine(
                                 "\n" +
                            "cmd - Info about the console\n" +
                            "help - Type \"help <command>\" for a more detailed explanation on any command\n" +
                            "driveinfo - Info on drives and removable media\n" +
                            "sysinfo - Info on your PC\n" +
                            "title - Changes window title\n" +
                            "exit - Exits the console\n" +
                            "del - Deletes a file\n" +
                            "ping - Pings an ip or website\n" +
                            "shorten - Shortens a url\n" +
                            "coin - Flips a coin\n" +
                            "cl - Clears the console"
                                );
                        }
                        else
                        {
                            switch (args[1])
                            {
                                case "cmd":
                                    Console.WriteLine("cmd\nShows ASCII logo and info on the console\n");
                                    break;

                                case "help":
                                    Console.WriteLine("no\n");
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
                            }

                        }
                        break;

                    case "driveinfo":

                        string drives = "";

                        Console.WriteLine("fetching drives...");

                        foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                        {
                            try
                            {
                                drives += $"\nDrive: {DriveInfo1.Name}\n [Volume Label: {DriveInfo1.VolumeLabel}\n [Type: {DriveInfo1.DriveType}\n [Format: {DriveInfo1.DriveFormat}\n [Total Size: {DriveInfo1.TotalSize} bytes\n [Free Space: {DriveInfo1.AvailableFreeSpace} bytes\n";
                            }
                            catch
                            {
                                Console.WriteLine("something went wrong");
                            }
                        }
                        Console.Clear();
                        Console.Write(user + "> driveinfo\n");
                        Console.WriteLine(drives);
                        break;

                    case "sysinfo":

                        string sysi = "";

                        Console.WriteLine("fetching info...");

                        try
                        {
                            sysi += $"\nOS Version: {OSVersion}\n [Processor Architecture: {ProcessorArchitecture}\n [Processor Identifier: {ProcessorIdentifier}\n [Processor Level: {ProcessorLevel}\n [System Directory: {SystemDirectory}\n [Processor Count: {ProcessorCount}\n [User Domain Name: {UserDomainName}\n [Username: {UserName}\n [Version: {Version}\n";
                        }
                        catch
                        {
                            Console.WriteLine("Something went wrong");
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


                        // yes ik this is inefficient but idc
                        if (File.Exists(DelPath) || Directory.Exists(DelPath))
                        {

                            if (DelPath.ToLower().Contains("c:\\windows"))
                            {
                                Console.Write("file MIGHT be a system file, are you sure you want to continue? (y/n)");

                                ConsoleKeyInfo SysfileConfirm = Console.ReadKey();

                                if (SysfileConfirm.Key == ConsoleKey.Y)
                                {
                                    Console.WriteLine("you might have to run as administrator to delete this file\n");
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            try
                            {
                                File.Delete(DelPath);
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
                                catch
                                {
                                    Console.WriteLine("failed to delete file or folder, make sure another app isnt using it");
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
                            Console.WriteLine("Usage: title <new window title>");
                            break;
                        }
                        string wintitle;

                        wintitle = args[1];

                        if (wintitle == "")
                        {
                            Console.WriteLine("nothing provided");
                            break;
                        }

                        Console.WriteLine($"New window title: {wintitle}\n");
                        break;

                    case "shorten":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: shorten <url>");
                            break;
                        }
                        if (args.Length >= 3) {
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

                    // vvv testing vvv
                    case "timever":
                        var parse = (DateTime.Today);
                        string ParseString = (Convert.ToString(parse));
                        string Date = ParseString[0] + "" /* prevents "cant convert type int to string" errors */ + ParseString[1] + ParseString[2] + ParseString[3] + ParseString[4] + ParseString[5] + ParseString[6] + ParseString[7];
                        string[] verArray = (Date.Split('/'));
                        if (verArray[0].Length < 2)
                        {
                            verArray[0] = "0" + verArray[0];
                        }
                        if (verArray[1].Length < 2)
                        {
                            verArray[1] = "0" + verArray[1];
                        }
                        if (verArray[2].Length < 2)
                        {
                            verArray[2] = "0" + verArray[2];
                        }
                        string Final = verArray[0] + verArray[1] + verArray[2];
                        Console.WriteLine("Build #" + Final);
                        break;
                    // ^^^ testing ^^^

                    case "":
                        Console.WriteLine("");
                        break;

                    case "cl":
                        Console.Clear();
                        break;

                    case "exit":
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
                    Console.WriteLine($"failed to ping {host}\nerror: " + e.Message);
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
        public static void RefreshWinTitle()
        {
            while (true)
            {
                var Time = (DateTime.Now);
                Console.Title = "oConsole | " + Time;
            } 
        }
    }
}
                            if (DelPath.ToLower().Contains("c:\\windows"))
                            {
                                Console.Write("file MIGHT be a system file, are you sure you want to continue? (y/n)");

                                ConsoleKeyInfo SysfileConfirm = Console.ReadKey();

                                if (SysfileConfirm.Key == ConsoleKey.Y)
                                {
                                    Console.WriteLine("you might have to run as administrator to delete this file\n");
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            try
                            {
                                File.Delete(DelPath);
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
                                catch
                                {
                                    Console.WriteLine("failed to delete file or folder, make sure another app isnt using it");
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
                            Console.WriteLine("Usage: title <new window title>");
                            break;
                        }
                        string wintitle;

                        wintitle = args[1];

                        if (wintitle == "")
                        {
                            Console.WriteLine("nothing provided");
                            break;
                        }

                        Console.Title = (wintitle);
                        Console.WriteLine($"New window title: {wintitle}\n");
                        break;

                    case "shorten":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: shorten <url>");
                            break;
                        }
                        if (args.Length >= 3) {
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

                    case "time":
                        Console.WriteLine(DateTime.Now);
                        break;
                    // vvv testing vvv
                    case "timever":
                        var parse = (DateTime.Today);
                        string ParseString = (Convert.ToString(parse));
                        string Date = ParseString[0] + "" /* prevents "cant convert type int to string" errors */ + ParseString[1] + ParseString[2] + ParseString[3] + ParseString[4] + ParseString[5] + ParseString[6] + ParseString[7];
                        string[] verArray = (Date.Split('/'));
                        if (verArray[0].Length < 2)
                        {
                            verArray[0] = "0" + verArray[0];
                        }
                        string Final = verArray[0] + verArray[1] + verArray[2];
                        Console.WriteLine("Build #" + Final);
                        break;

                    case "":
                        Console.WriteLine("");
                        break;

                    case "cl":
                        Console.Clear();
                        break;

                    case "exit":
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
            if (!(host == "localhost") && !host.Contains(".") && !host.Contains(":") || host == "")
            {
                Console.WriteLine("thats not an ip or url...");
            }
            else
            {
                try
                {
                    if (host == "localhost")
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
                           + "Response delay
