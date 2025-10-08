using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace shitass
{
    public class HashCommand : ICommand
    {
        public string Name => "hash";
        public string[] Aliases => new string[] { };
        public string Description => "Calculate file hash (MD5/SHA256)";
        public string Usage => "hash <filepath> [md5|sha256]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: hash <filepath> [md5|sha256]");
                return Task.CompletedTask;
            }

            string filePath = string.Join(" ", args.Skip(1).Take(args.Length - (args.Length > 2 ? 2 : 1)));
            string algorithm = args.Length > 2 ? args[args.Length - 1].ToLower() : "sha256";

            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(Environment.CurrentDirectory, filePath);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return Task.CompletedTask;
            }

            try
            {
                using (var stream = File.OpenRead(filePath))
                {
                    string hash;
                    if (algorithm == "md5")
                    {
                        using (var md5 = MD5.Create())
                        {
                            byte[] hashBytes = md5.ComputeHash(stream);
                            hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                        }
                    }
                    else // sha256
                    {
                        using (var sha = SHA256.Create())
                        {
                            byte[] hashBytes = sha.ComputeHash(stream);
                            hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                        }
                    }

                    Console.WriteLine($"\n{algorithm.ToUpper()}: {hash}\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
