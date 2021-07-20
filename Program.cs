using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Net.Http;
using System.Diagnostics;
//using System.Runtime.InteropServices;

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
            Console.Title = ("Shitass");

            Console.WriteLine("loading...");

            string user = Environment.UserName;

            string first = "C:\\Users\\" + user + "\\AppData\\Local\\Temp\\.SHITASS";
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

            // wtf rat confirmed!

            bool firstrun = !File.Exists(first);

            if (firstrun)
            {
                try
                {
                    File.Create(first);
                }
                catch
                {
                    Console.WriteLine($"failed to create file at {first}, try running as administrator\n");
                    Thread.Sleep(7500);
                }
            }

            string CurrentDir = $"C:\\Users\\{user}\\";

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
                            "Shitass Console Build 6\n" +
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
                            "coin - flips a coin\n"
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
                                    Console.WriteLine("driveinfo\nshows info on drives and removable media\n");
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
                                drives += $"\nDrive: {DriveInfo1.Name}\n [VolumeLabel: {DriveInfo1.VolumeLabel}\n [Type: {DriveInfo1.DriveType}\n [Format: {DriveInfo1.DriveFormat}\n [Total Size: {DriveInfo1.TotalSize} bytes\n [Free Space: {DriveInfo1.AvailableFreeSpace} bytes\n";
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

                        if (DelPath.ToLower().Contains("c:\\windows"))
                        {
                            Console.Write("file MIGHT be a system file, are you sure you want to continue? (y/n)");

                            string SysfileConfirm = Console.ReadLine();

                            if (SysfileConfirm == "y")
                            {
                                Console.WriteLine("you might have to run as administrator to delete this file\n");
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (File.Exists(DelPath))
                        {
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
                        if (args[2] == "/alt")
                        {
                            try
                            {
                                await shorten(args[1], true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                await shorten(args[1], false);
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

                    case "":
                        Console.WriteLine("");
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
            try
            {
                r = p.Send(host);

                if (r.Status == IPStatus.Success)
                {
                    Console.WriteLine("Ping to " + host.ToString() + "\n[" + r.Address.ToString() + "]\n" + "Status: Successful\n"
                       + "Response delay = " + r.RoundtripTime.ToString() + " ms\n");
                }
                else
                {
                    Console.WriteLine($"failed to ping {host}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed to ping {host}\nerror: " + e.Message);
            }
        }
        public static async Task<string> shorten(string url, bool alt)
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
    }
}
// kys
