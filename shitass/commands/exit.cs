using System;
using System.Threading.Tasks;

namespace shitass
{
    class ExitCommand : ICommand
    {
        public string Name => "exit";
        public string[] Aliases => new string[] { "bye", "quit", "goodbye", "kys", "killyourself" };
        public string Description => "Closes the console";
        public string Usage => "exit";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            Environment.Exit(0);
            return Task.CompletedTask; // Thanks c#
        }
    }
}
