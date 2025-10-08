using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class CatCommand : ICommand
    {
        public string Name => "cat";
        public string[] Aliases => new string[] { "type" };
        public string Description => "Display file contents";
        public string Usage => "cat <filepath>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: cat <filepath>");
                return Task.CompletedTask;
            }

            string path = string.Join(" ", args.Skip(1));

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File not found.");
                    return Task.CompletedTask;
                }

                string content = File.ReadAllText(path);
                Console.WriteLine($"\n{content}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
