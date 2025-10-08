using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class OpenCommand : ICommand
    {
        public string Name => "open";
        public string[] Aliases => new string[] { "start" };
        public string Description => "Opens a file or folder in default application";
        public string Usage => "open <path>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                System.Diagnostics.Process.Start("explorer.exe", Environment.CurrentDirectory);
                return Task.CompletedTask;
            }

            string path = string.Join(" ", args.Skip(1));

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }

            try
            {
                if (Directory.Exists(path))
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else if (File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
                else
                {
                    Console.WriteLine("File or directory not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
