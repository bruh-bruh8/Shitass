using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shitass.commands
{
    class RpsCommand : ICommand
    {
        public string Name => "rps";
        public string[] Aliases => new string[] {"rockpaperscissors"};
        public string Description => "Play rock paper scissors";
        public string Usage => "rps [rock|paper|scissors]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: rps <rock|paper|scissors>");
                return Task.CompletedTask;
            }
            rps(args[1].ToLower());
            return Task.CompletedTask;
        }
        public static void rps(string playerChoice)
        {
            string[] choices = { "rock", "paper", "scissors" };
            Random rng = new Random();
            string computerChoice = choices[rng.Next(choices.Length)];

            if (Array.Exists(choices, choice => choice == playerChoice))
            {
                Console.WriteLine($"you chose {playerChoice}, i chose {computerChoice}");

                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("it's a tie");
                }
                else if ((playerChoice == "rock" && computerChoice == "scissors") ||
                            (playerChoice == "paper" && computerChoice == "rock") ||
                            (playerChoice == "scissors" && computerChoice == "paper"))
                {
                    Console.WriteLine("you win");
                }
                else
                {
                    Console.WriteLine("you lose");
                }
            }
            else
            {
                Console.WriteLine("Usage: rps <rock|paper|scissors>");
                return;
            }
        }
    }
}
