using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class DeleteCommand : ICommand
    {
        public string Name => "del";
        public string[] Aliases => new string[] { "delete", "rm", "rmdir" };
        public string Description => "Deletes a file or directory";
        public string Usage => "del <path>";

        private static readonly string[] DangerousPaths = new string[]
        {
            "system32",
            "windows",
            "program files",
            "programdata",
            "boot",
        };

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Usage: del <path>");
                return Task.CompletedTask;
            }

            string path = string.Join(" ", args.Skip(1));

            try
            {
                // Make path relative to current directory if not absolute
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(Environment.CurrentDirectory, path);
                }

                // Get absolute path for checking
                string fullPath = Path.GetFullPath(path).ToLower();


                bool isDangerous = DangerousPaths.Any(dangerous =>
                    fullPath.Contains(dangerous.ToLower()));

                if (isDangerous)
                {
                    Console.WriteLine("\n!!! WARNING !!!");
                    Console.WriteLine($"You're about to delete: {fullPath}");
                    Console.WriteLine("This looks like a system directory. Deleting this will likely break Windows.");
                    Console.Write("\nType 'I KNOW WHAT IM DOING' to confirm: ");

                    string confirmation = Console.ReadLine();

                    if (confirmation != "I KNOW WHAT IM DOING")
                    {
                        Console.WriteLine("Deletion cancelled");
                        return Task.CompletedTask;
                    }

                    Console.WriteLine("\nGood luck\n");
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine($"Successfully deleted file: {path}");
                }
                else if (Directory.Exists(path))
                {
                    var fileCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
                    if (fileCount > 10)
                    {
                        Console.WriteLine($"\nThis directory contains {fileCount} files.");
                        Console.Write("Are you sure? (y/n): ");
                        if (Console.ReadLine()?.ToLower() != "y")
                        {
                            Console.WriteLine("Deletion cancelled");
                            return Task.CompletedTask;
                        }
                    }

                    Directory.Delete(path, true);
                    Console.WriteLine($"Successfully deleted directory: {path}");
                }
                else
                {
                    Console.WriteLine("file doesnt exist bruh...");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Deletion failed, you probably don't have permission");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting {path}: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}