using System;
using System.Threading.Tasks;

namespace shitass
{
    class ClearCommand : ICommand
    {
        public string Name => "clear";
        public string[] Aliases => new string[] { "cl" };
        public string Description => "clears the console";
        public string Usage => "clear";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            Console.Clear();
            if (context.Settings.ShowStartupMessage)
            {
                Console.WriteLine("shitass\n");
            }
            return Task.CompletedTask;
        }
    }
}
