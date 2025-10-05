using System;
using System.Threading.Tasks;

namespace shitass
{
    class SysInfoCommand : ICommand
    {
        public string Name => "sysinfo";
        public string[] Aliases => new string[] { "systeminfo", "wtfcomputer" };
        public string Description => "Shows info on your computer";
        public string Usage => "sysinfo";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
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
            var CLRVersion = Environment.Version;
            var totalRAM = GetTotalMemory();  // ffuckkkkkk
            var uptime = TimeSpan.FromMilliseconds(Environment.TickCount);
            var domainName = Environment.UserDomainName;

            try
            { // I made it actually readable
                sysi += $"\nOS: {OS}\n";
                sysi += $" [OS Version: {OSVersion}\n";
                sysi += $" [64-bit: {bit}\n";
                sysi += $" [Computer Name: {PCName}\n";
                sysi += $" [Domain: {domainName}\n";
                sysi += $" [Username: {Environment.UserName}\n";
                sysi += $" [System Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m\n";
                sysi += $"\n";
                sysi += $" [Processor: {ProcessorIdentifier}\n";
                sysi += $" [Processor Architecture: {ProcessorArchitecture}\n";
                sysi += $" [Core Count: {ProcessorCount}\n";
                sysi += $" [Total RAM: {totalRAM} GB\n";
                sysi += $"\n";
                sysi += $" [Main Drive: {Letter}\n";
                sysi += $" [System Directory: {SystemDirectory}\n";
                sysi += $" [Current Directory: {CurrentDirectory}\n";
                sysi += $" [CLR Version: {CLRVersion}\n";
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
            }
            Console.WriteLine(sysi);
            return Task.CompletedTask;
        }
        public static string GetTotalMemory()
        {
            try
            {
                using (var searcher = new System.Management.ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem")) // i dont fucking know
                {
                    foreach (var obj in searcher.Get())
                    {
                        double totalBytes = Convert.ToDouble(obj["TotalPhysicalMemory"]);
                        double totalGB = totalBytes / (1024.0 * 1024.0 * 1024.0);
                        return $"{totalGB:F2}";
                    }
                }
            }
            catch
            {
                return "Unknown";
            }
            return "Unknown";
        }
    }
}
