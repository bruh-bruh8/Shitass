using System;
using System.Threading.Tasks;

namespace shitass
{
    class DriveInfoCommand : ICommand
    {
        public string Name => "driveinfo";
        public string[] Aliases => new string[] { "drives", "df"};
        public string Description => "Shows info on drives and removable media";
        public string Usage => "driveinfo";
        public Task ExecuteAsync(string[] args, CommandContext context)
        {
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
            return Task.CompletedTask;
        }
    }
}
