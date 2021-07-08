using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
//using System.Runtime;
//using System.Runtime.InteropServices;

namespace SHITASS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = ("Shitass");
            string user = Environment.UserName;

            Console.WriteLine("shitass\n");

            while (true)
            {

                Console.Write(user + "> ");

                string cmd;
                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "cmd":
                        Console.WriteLine(
                            "       .__    .__  __                        \n" +
                            "  _____|  |__ |__|/  |______    ______ ______\n" +
                            " /  ___/  |  \\|  \\   __\\__  \\  /  ___//  ___/\n" +
                            " \\___ \\|   Y  \\  ||  |  / __ \\_\\___ \\ \\___ \\ \n" +
                            "/____  >___|  /__||__| (____  /____  >____  >\n" +
                            "     \\/     \\/              \\/     \\/     \\/ \n" +
                            "Shitass Console alpha\\b3\n" +
                            "made by orange\n"
                            );
                        break;

                    case "help":
                        Console.WriteLine(
                            "\n" +
                            "cmd - fancy logo\n" +
                            "help - help\n" +
                            "driveinfo - info on yo drives\n" +
                            "sysinfo - info on yo pc\n" +
                            "title - changes window title\n" +
                            "exit - who the fuck knows\n"
                            );
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
                        Console.WriteLine(drives);
                        break;

                    case "sysinfo":

                    string systemi = "";

                    Console.WriteLine("fetching info...");
                        // ill make this better later
                        var OSVersion = Environment.OSVersion;
                        var ProcessorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                        var ProcessorIdentifier = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                        var ProcessorLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL");
                        var SystemDirectory = Environment.SystemDirectory;
                        var ProcessorCount = Environment.ProcessorCount;
                        var UserDomainName = Environment.UserDomainName;
                        var UserName = Environment.UserName;
                        var Version = Environment.Version;

                        try
                        {
                            systemi += $"\nOS Version: {OSVersion}\n [Processor Architecture: {ProcessorArchitecture}\n [Processor Identifier: {ProcessorIdentifier}\n [Processor Level: {ProcessorLevel}\n [System Directory: {SystemDirectory}\n [Processor Count: {ProcessorCount}\n [User Domain Name: {UserDomainName}\n [Username: {UserName}\n [Version: {Version}\n";
                        }
                        catch
                        {
                            Console.WriteLine("something went wrong");
                        }
                    Console.WriteLine(systemi);
                    break;

                    case "title":
                        Console.Write("new window title: ");

                        string wintitle;
                        wintitle = Console.ReadLine();

                        if (wintitle == "")
                        {
                            Console.WriteLine("nothing provided");
                            break;
                        }

                        Console.Title = (wintitle);
                        break;

                    case "":
                        Console.WriteLine("");
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    case "stop":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("eric command fail");
                        break;
                }

            }

        }
    }
}