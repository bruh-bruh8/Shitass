using System;
using System.Threading.Tasks;

namespace shitass
{
    public class WhoCommand : ICommand
    {
        public string Name => "whoami";
        public string[] Aliases => new string[] {"whotfami"};
        public string Description => "Display current username";
        public string Usage => "whoami";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            Console.WriteLine($"{Environment.UserDomainName}\\{Environment.UserName}");
            return Task.CompletedTask;
        }
    }
}