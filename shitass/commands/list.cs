using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class LsCommand : ICommand
    {
        public string Name => "ls";
        public string[] Aliases => new string[] { "list" };
        public string Description => "List files and directories";
        public string Usage => "ls [path]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            string path = args.Length > 1
                ? string.Join(" ", args.Skip(1))
                : Environment.CurrentDirectory;

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }

            try
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);

                Console.WriteLine($"\nDirectory: {path}\n");

                if (dirs.Length > 0)
                {
                    Console.WriteLine("Directories:");
                    foreach (var dir in dirs)
                    {
                        Console.WriteLine($"  [DIR]  {Path.GetFileName(dir)}");
                    }
                    Console.WriteLine();
                }

                if (files.Length > 0)
                {
                    Console.WriteLine("Files:");
                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        double sizeMB = fileInfo.Length / (1024.0 * 1024.0);
                        string sizeStr = sizeMB > 1
                            ? $"{sizeMB:F2} MB"
                            : $"{fileInfo.Length / 1024.0:F2} KB";

                        Console.WriteLine($"  {Path.GetFileName(file),-40} {sizeStr,10}");
                    }
                }

                Console.WriteLine($"\n{dirs.Length} directories, {files.Length} files\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
