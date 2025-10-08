using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class MkdirCommand : ICommand
    {
        public string Name => "mkdir";
        public string[] Aliases => new string[] { "md" };
        public string Description => "Create a directory";
        public string Usage => "mkdir <path>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: mkdir <path>");
                return Task.CompletedTask;
            }

            string path = string.Join(" ", args.Skip(1));

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }

            try
            {
                Directory.CreateDirectory(path);
                Console.WriteLine($"Created directory: {path}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
