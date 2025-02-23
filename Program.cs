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
        const string version = "250223"; // current build (remember to update lol)
        static async Task Main()
        {
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
                string[] settings = { "version=250223", "wintitle=shitass", "bgcolor=Black", "fgcolor=White" };
                System.IO.File.WriteAllLines(SettingsPath, settings, Encoding.UTF8);
                Console.WriteLine($"Created settings file at {SettingsPath}");
                Thread.Sleep(3000);
            }
            if (System.IO.File.ReadLines(SettingsPath).First().Split('=')[1] != version) {
                File.Delete(SettingsPath);
                var settingfile = System.IO.File.Create(SettingsPath);
                settingfile.Close();
                string[] settings = { "version=250223", "wintitle=shitass", "bgcolor=Black", "fgcolor=White" };
                System.IO.File.WriteAllLines(SettingsPath, settings, Encoding.UTF8);
                Console.WriteLine($"Old version detected, your settings may have been reset.");
                Thread.Sleep(3000);
            }
            string WinTitle = System.IO.File.ReadLines(SettingsPath).Skip(1).First().Split('=')[1];
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(2).First().Split('=')[1], true);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), System.IO.File.ReadLines(SettingsPath).Skip(3).First().Split('=')[1], true);
            Thread title = new Thread(() => RefreshWinTitle(WinTitle));
            title.Start();
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
                    case "shitass":
                    case "info":
                    case "version":
                        Console.WriteLine("         __    _ __                 \n" +
                        "   _____/ /_  (_) /_____ ___________\n" +
                        "  / ___/ __ \\/ / __/ __ `/ ___/ ___/\n" +
                        " (__  ) / / / / /_/ /_/ (__  |__  ) \n" +
                        "/____/_/ /_/_/\\__/\\__,_/____/____/  \n" +
                        "                                    \n" +
                        "Shitass b250220, " +
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
                            "cl - Clears the console\n" +
                            "color - Changes color of console elements\n" +
                            "ascii - Creates ascii text art\n" +
                            "rps - Plays rock paper scissors\n"
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
                                    Console.WriteLine("shorten\nShortens a url\nOptional args: -alt: uses v.gd instead of is.gd\n");
                                    break;

                                case "coin":
                                    Console.WriteLine("coin\nFlips a coin");
                                    break;

                                case "cl":
                                    Console.WriteLine("cl\nClears the screen");
                                    break;

                                case "color":
                                    Console.WriteLine("color\nChanges the color of the console background/text\nOptional args: -reset: resets colors");
                                    break;

                                case "ascii":
                                    Console.WriteLine("ascii\nCreates ascii text art");
                                    break;

                                case "rps":
                                    Console.WriteLine("rps\nPlays rock paper scissors");
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

                    case "delete":
                    case "del":

                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: del <path>");
                            break;
                        }

                        string DelPath = args[1];

                        try
                        {
                            if (File.Exists(DelPath))
                            {
                                File.Delete(DelPath);
                                Console.WriteLine($"Successfully deleted file: {DelPath}");
                            }
                            else if (Directory.Exists(DelPath))
                            {
                                Directory.Delete(DelPath, true); // recursive true now
                                Console.WriteLine($"Successfully deleted directory: {DelPath}");
                            }
                            else
                            {
                                Console.WriteLine("file doesnt exist bruh...");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error deleting {DelPath}: {e.Message}");
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
                    case "wintitle":
                    case "windowtitle":
                        
                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: title <new window title> OR title -reset");
                            break;
                        }
                        if (args[1] == "-reset")
                        {
                            string[] lines = File.ReadAllLines(SettingsPath);
                            lines[1] = "wintitle=shitass";
                            File.WriteAllLines(SettingsPath, lines);
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
                            lines[1] = "wintitle=" + wintitle;
                            File.WriteAllLines(SettingsPath, lines);
                            Console.WriteLine("Restart for your changes to take effect");
                        }
                        break;

                    case "color":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Usage: color background/text <color> OR color -reset");
                            Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                            break;
                        }

                        string property = args[1].ToLower();
                        string color = args.Length > 2 ? args[2] : "";

                        try
                        {
                            string[] lines = File.ReadAllLines(SettingsPath);

                            if (property == "-reset")
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                lines[2] = "bgcolor=black";
                                lines[3] = "fgcolor=white";
                                File.WriteAllLines(SettingsPath, lines);
                                Console.Clear();
                                Console.WriteLine("shitass\n");
                                Console.WriteLine("Reset console colors");
                                break;
                            }

                            switch (property)
                            {
                                case "foreground":
                                case "fg":
                                case "text":
                                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    Console.WriteLine($"Changed foreground color to {color}");
                                    lines[3] = "fgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    Console.Clear();
                                    Console.WriteLine("shitass\n");
                                    break;
                                    
                                case "background":
                                case "bg":
                                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                                    lines[2] = "bgcolor=" + color;
                                    File.WriteAllLines(SettingsPath, lines);
                                    Console.Clear();
                                    Console.WriteLine("shitass\n");
                                    Console.WriteLine($"Changed background color to {color}\n");
                                    Console.Clear();
                                    Console.WriteLine("shitass\n");
                                    break;

                                case "preset":
                                    switch (color)
                                    {
                                        case "light": // todo: make console self destruct if selected
                                            Console.BackgroundColor = ConsoleColor.White;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            lines[2] = "bgcolor=white";
                                            lines[3] = "fgcolor=black";
                                            File.WriteAllLines(SettingsPath, lines);
                                            Console.Clear();
                                            Console.WriteLine("shitass\n");
                                            break;

                                        case "dark":
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            lines[2] = "bgcolor=black";
                                            lines[3] = "fgcolor=white";
                                            File.WriteAllLines(SettingsPath, lines);
                                            Console.Clear();
                                            Console.WriteLine("shitass\n");
                                            break;

                                        case "hackerman":
                                        case "skid":
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            lines[2] = "bgcolor=black";
                                            lines[3] = "fgcolor=green";
                                            File.WriteAllLines(SettingsPath, lines);
                                            Console.Clear();
                                            Console.WriteLine("shitass\n");
                                            break;
                                    }
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
                                try
                                {
                                    await Shorten(args[1], true);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
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

                    case "password":
                        if (args.Length <= 1)
                        {
                            Console.WriteLine("Usage: password <length>");
                            break;
                        }
                        if (int.TryParse(args[1], out int length))
                        {
                            genPassword(length);
                        }
                        else
                        {
                            Console.WriteLine("Invalid length. Please enter a number.");
                        }
                        break;

                    case "coin":
                    case "coinflip":
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

                    case "ascii":
                    case "textart":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Usage: ascii <text> [font]");
                            break;
                        }
                        if (string.IsNullOrWhiteSpace(args[1]))
                        {
                            Console.WriteLine("Usage: ascii <text> [font]");
                            return;
                        }
                        string asciiText = args.Length >= 3 ? string.Join(" ", args.Skip(1).Take(args.Length - 2)) : string.Join(" ", args.Skip(1));
                        string font = args.Length >= 3 ? args[2] : "";

                        await genAscii(asciiText, font);
                        break;

                    case "rps":
                    case "rockpaperscissors":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Usage: rps <rock|paper|scissors>");
                            break;
                        }
                        rps(args[1].ToLower());
                        break;

                    case "":
                        Console.WriteLine("");
                        break;

                    case "cl":
                    case "clear":
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        break;

                    case "exit":
                    case "quit":
                    case "kys":
                    case "killyourself":
                        Environment.Exit(0);
                        break;

                    case "restart":
                        Console.Clear();
                        await Main();
                    break;

                    default:
                        Console.WriteLine("wtf you saying bruh..");
                        break;
                }
            }
        }
        public static void ping(string host)
        {
            if (string.IsNullOrWhiteSpace(host) || host.Length < 7 || host.Length > 39 ||
        (!host.Contains(".") && !host.Contains(":") && !host.Equals("localhost", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid IP address or hostname.");
                return;
            }

            try
            {
                using (Ping p = new Ping())
                {
                    string target = host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ? "127.0.0.1" : host;
                    PingReply reply = p.Send(target);

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"Ping to {host} [{reply.Address}]\n" +
                                          $"Status: Successful\n" +
                                          $"Response Time: {reply.RoundtripTime} ms\n");
                    }
                    else
                    {
                        Console.WriteLine($"Ping failed: {reply.Status}");
                    }
                }
            }
            catch (PingException pe)
            {
                Console.WriteLine($"Ping failed: {pe.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
        public static async Task<string> Shorten(string url, bool alt)
        {
            string baseUrl = alt ? "https://v.gd/create.php" : "https://is.gd/create.php";
            string uri = $"{baseUrl}?format=simple&url={Uri.EscapeDataString(url)}&logstats=1";

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string response = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error: {responseMessage.StatusCode} - {response}");

                Console.WriteLine($"\n{response}\n");
                return response;
            }
        }
        public static void RefreshWinTitle(string t)
        {
            while (true)
            {
                Console.Title = t + " | " + DateTime.Now;
                Thread.Sleep(50);
            }
        }
        public static async Task genAscii(string text, string font = "") // https://github.com/thelicato/asciified
        {

            string url = $"https://asciified.thelicato.io/api/v2/ascii?text={Uri.EscapeDataString(text)}";
            if (!string.IsNullOrWhiteSpace(font))
            {
                url += $"&font={Uri.EscapeDataString(font)}"; // font is case sensitive so kinda jank (i added support tho such good dev)
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error getting ascii art: {response.StatusCode}");
                        return;
                    }

                    string asciiArt = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\n" + asciiArt + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

        }
        public static void genPassword(int length = 8)
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            char[] password = new char[length];

            Random r = new Random();

            for (int i = 0; i < length; i++) {
                switch (r.Next(1, 5))
                {
                    case 1:
                        password[i] = uppercase[r.Next(uppercase.Length)];
                    break;
                    case 2:
                        password[i] = lowercase[r.Next(lowercase.Length)];
                        break;
                    case 3:
                        password[i] = numbers[r.Next(numbers.Length)];
                        break;
                    case 4:
                        password[i] = symbols[r.Next(symbols.Length)];
                        break;
                }
            }
            bool hasUpper = false, hasLower = false, hasNumber = false, hasSymbol = false;
            while (!hasUpper || !hasLower || !hasNumber || !hasSymbol)
            {
                foreach (char c in password)
                {
                    if (uppercase.Contains(c)) hasUpper = true;
                    else if (lowercase.Contains(c)) hasLower = true;
                    else if (numbers.Contains(c)) hasNumber = true;
                    else if (symbols.Contains(c)) hasSymbol = true;
                }
                if (!hasUpper)
                {
                    password[r.Next(password.Length)] = uppercase[r.Next(uppercase.Length)];
                }
                if (!hasLower)
                {
                    password[r.Next(password.Length)] = lowercase[r.Next(lowercase.Length)];
                }
                if (!hasNumber)
                {
                    password[r.Next(password.Length)] = numbers[r.Next(numbers.Length)];
                }
                if (!hasSymbol)
                {
                    password[r.Next(password.Length)] = symbols[r.Next(symbols.Length)];
                }
            }
            
            Console.WriteLine($"Password: {new string (password)}");
        }
        public static void rps(string playerChoice)
            {
                string[] choices = { "rock", "paper", "scissors" };
                Random rng = new Random();
                string computerChoice = choices[rng.Next(choices.Length)];

                if (!Array.Exists(choices, choice => choice == playerChoice))
                {
                    Console.WriteLine("Usage: rps <rock|paper|scissors>");
                    return;
                }

                Console.WriteLine($"you chose {playerChoice}, i chose {computerChoice}");

                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("it's a tie");
                }
                else if ((playerChoice == "rock" && computerChoice == "scissors") ||
                         (playerChoice == "paper" && computerChoice == "rock") ||
                         (playerChoice == "scissors" && computerChoice == "paper"))
                {
                    Console.WriteLine("you win");
                }
                else
                {
                    Console.WriteLine("you lose");
                }
            }
        }
}
