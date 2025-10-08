using System;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    class HelpCommand : ICommand
    {
        private readonly CommandRegistry _registry;

        public HelpCommand(CommandRegistry registry)
        {
            _registry = registry;
        }

        public string Name => "help";
        public string[] Aliases => new string[] { "man" };
        public string Description => "Shows available commands";
        public string Usage => "help [command]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine();
                foreach (var cmd in _registry.GetAllCommands().OrderBy(c => c.Name))
                {
                    Console.WriteLine($"{cmd.Name} - {cmd.Description}");
                }
                Console.WriteLine("\nType \"help <command>\" for more details\n");
            }
            else
            {
                var cmd = _registry.GetCommand(args[1]);
                if (cmd != null)
                {
                    Console.WriteLine($"\n{cmd.Name}");
                    Console.WriteLine($"{cmd.Description}");
                    Console.WriteLine($"Usage: {cmd.Usage}");
                    if (cmd.Aliases.Length > 0)
                    {
                        Console.WriteLine($"Aliases: {string.Join(", ", cmd.Aliases)}");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Not a valid command");
                }
            }
            return Task.CompletedTask;
        }
    }
}
