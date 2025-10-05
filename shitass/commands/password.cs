using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shitass
{
    class PasswordCommand : ICommand
    {
        public string Name => "password";
        public string[] Aliases => new string[] { "pw" };
        public string Description => "Generates you a password";
        public string Usage => "password [length]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Usage: password <length>");
                return Task.CompletedTask;
            }
            if (int.TryParse(args[1], out int length))
            {
                genPassword(length);
            }
            else
            {
                Console.WriteLine("Invalid length. Please enter a number.");
            }
            return Task.CompletedTask;
        }
        public static void genPassword(int length = 8)
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            char[] password = new char[length];

            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                switch (r.Next(1, 5))
                {
                    case 1:
                        password[i] = uppercase[r.Next(uppercase.Length)];
                        break;
                    case 2:
                        password[i] = lowercase[r.Next(lowercase.Length)];
                        break;
                    case 3:
                        password[i] = numbers[r.Next(numbers.Length)];
                        break;
                    case 4:
                        password[i] = symbols[r.Next(symbols.Length)];
                        break;
                }
            }

            Console.WriteLine($"Password: {new string(password)}");
        }
    }
}
