using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Web;
using System.Net.NetworkInformation;
using System.Net.Http;
using NeoSmart.Utils;


// shoutout plex https://github.com/plexthedev

/* todo
 * 
 * base64 converter
 * 
 */

namespace SHITASS
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = ("Loading...");
            Console.WriteLine("loading...");

            string user = Environment.UserName;

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
            Thread thread1 = new Thread(Program.RefreshWinTitle);
            thread1.Start();
            Console.Clear();

            Console.WriteLine("shitass\n");
            

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
                            "       .__    .__  __                        \n" +
                            "  _____|  |__ |__|/  |______    ______ ______\n" +
                            " /  ___/  |  \\|  \\   __\\__  \\  /  ___//  ___/\n" +
                            " \\___ \\|   Y  \\  ||  |  / __ \\_\\___ \\ \\___ \\ \n" +
                            "/____  >___|  /__||__| (____  /____  >____  >\n" +
                            "     \\/     \\/              \\/     \\/     \\/ \n" +
                            "Shitass Console [Build 7]\n" +
                            "made by orange\n"
                            );
                        break;
                    case "help":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine(
                                 "\n" +
                            "cmd - fancy logo\n" +
                            "help - type help <command> for a more detailed explanation\n" +
                            "driveinfo - info on yo drives\n" +
                            "sysinfo - info on yo pc\n" +
                            "title - changes window title\n" +
                            "exit - who the fuck knows\n" +
                            "del - deletes a file\n" +
                            "ping - pings an ip or website\n" +
                            "shorten - shortens a url\n" +
                            "time - prints the time\n" +
                            "coin - flips a coin\n" +
                            "cl - clears the screen"
                                );
                        }
                        else
                        {
                            switch (args[1])
                            {
                                case "cmd":
                                    Console.WriteLine("cmd\nshows ascii logo and version\n");
                                    break;

                                case "help":
                                    Console.WriteLine("no\n");
                                    break;

                                case "driveinfo":
                                    Console.WriteLine("driveinfo\nshows info on drives and removable media (\n");
                                    break;

                                case "sysinfo":
                                    Console.WriteLine("sysinfo\nshows info on your system\n");
                                    break;

                                case "title":
                                    Console.WriteLine("title\nchanges the title of the command prompt\n");
                                    break;

                                case "exit":
                                    Console.WriteLine("exit\nexits the app\n");
                                    break;

                                case "del":
                                    Console.WriteLine("del\ndeletes a file, folders not supported\n");
                                    break;

                                case "ping":
                                    Console.WriteLine("ping\npings an ip or website and shows info\n");
                                    break;

                                case "shorten":
                                    Console.WriteLine("shorten\nshortens a url\noptional args: /alt: uses v.gd instead of is.gd\n");
                                    break;

                                case "time":
                                    Console.WriteLine("time\nprints the time");
                                    break;

                                case "coin":
                                    Console.WriteLine("coin\nflips a coin and prints heads or tails");
                                    break;

                                case "cl":
                                    Console.WriteLine("cl\nclears the screen");
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
                            Console.WriteLine("something went wrong");
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
                string ParseString = (Convert.ToString(Time));
                Console.Title = ("Shitass | " + ParseString);

            } 
        }
    }
}
// ye
