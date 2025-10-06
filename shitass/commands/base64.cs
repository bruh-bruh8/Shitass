using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shitass
{
    public class Base64Command : ICommand
    {
        public string Name => "base64";
        public string[] Aliases => new string[] { "b64" };
        public string Description => "Encode or decode base64";
        public string Usage => "base64 encode/decode <text>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: base64 encode/decode <text>");
                return Task.CompletedTask;
            }

            string operation = args[1].ToLower();
            string input = string.Join(" ", args.Skip(2));

            try
            {
                if (operation == "encode" || operation == "e")
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(input);
                    string encoded = Convert.ToBase64String(bytes);
                    Console.WriteLine($"\n{encoded}\n");
                }
                else if (operation == "decode" || operation == "d")
                {
                    byte[] bytes = Convert.FromBase64String(input);
                    string decoded = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine($"\n{decoded}\n");
                }
                else
                {
                    Console.WriteLine("Invalid operation. Use 'encode' or 'decode'.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid base64 string.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
