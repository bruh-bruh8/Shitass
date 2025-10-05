using System;
using System.Threading.Tasks;

namespace shitass
{
    class CoinflipCommand : ICommand
    {
        public string Name => "coin";
        public string[] Aliases => new[] { "coinflip" };
        public string Description => "Flips a coin";
        public string Usage => "coin";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            var result = new Random().Next(0, 2) == 0 ? "Heads" : "Tails";
            Console.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
