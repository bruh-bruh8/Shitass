using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class FindCommand : ICommand
    {
        public string Name => "find";
        public string[] Aliases => new string[] { "search" };
        public string Description => "Search for files by name";
        public string Usage => "find <pattern> [path]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: find <pattern> [path]");
                Console.WriteLine("Example: find *.txt");
                return Task.CompletedTask;
            }

            string pattern = args[1];
            string searchPath = args.Length > 2
                ? string.Join(" ", args.Skip(2))
                : Environment.CurrentDirectory;

            if (!Path.IsPathRooted(searchPath))
            {
                searchPath = Path.Combine(Environment.CurrentDirectory, searchPath);
            }

            try
            {
                Console.WriteLine($"\nSearching for '{pattern}' in {searchPath}...\n");

                var files = Directory.GetFiles(searchPath, pattern, SearchOption.AllDirectories);

                if (files.Length == 0)
                {
                    Console.WriteLine("No files found.");
                }
                else
                {
                    foreach (var file in files.Take(50)) // 50 limit, maybe make arg or setting later
                    {
                        Console.WriteLine(file);
                    }

                    if (files.Length > 50)
                    {
                        Console.WriteLine($"\n... and {files.Length - 50} more files");
                    }

                    Console.WriteLine($"\nFound {files.Length} file(s)\n");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to some directories.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
