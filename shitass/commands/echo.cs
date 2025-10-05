using System;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";
        public string[] Aliases => new string[] { "repeat", "say" };
        public string Description => "Outputs text to the console";
        public string Usage => "echo <text>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            string text = string.Join(" ", args.Skip(1));
            Console.WriteLine($"{text}");
            return Task.CompletedTask;
        }
    }
}