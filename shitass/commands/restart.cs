using System;
using System.Threading.Tasks;

namespace shitass
{
    class RestartCommand : ICommand
    {
        public string Name => "restart";
        public string[] Aliases => new string[] { };
        public string Description => "Restarts the console";
        public string Usage => "restart";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            Console.Clear();
            Console.WriteLine("Restarting...");

            System.Diagnostics.Process.Start(
                System.Reflection.Assembly.GetExecutingAssembly().Location
            );

            Environment.Exit(0);

            return Task.CompletedTask;
        }
    }
}
