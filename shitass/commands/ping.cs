using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace shitass
{
    class PingCommand : ICommand
    {
        public string Name => "ping";
        public string[] Aliases => new string[] { };
        public string Description => "pings an ip/website";
        public string Usage => "ping [ip/domain]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Usage: ping <ip or website>");
                return Task.CompletedTask;
            }
            ping(args[1]);
            return Task.CompletedTask;
        }
        public static void ping(string host)
        {
            if (string.IsNullOrWhiteSpace(host) || host.Length < 7 || host.Length > 39 ||
        (!host.Contains(".") && !host.Contains(":") && !host.Equals("localhost", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid IP address or hostname.");
                return;
            }

            try
            {
                using (Ping p = new Ping())
                {
                    string target = host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ? "127.0.0.1" : host;
                    PingReply reply = p.Send(target);

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"Ping to {host} [{reply.Address}]\n" +
                                          $"Status: Successful\n" +
                                          $"Response Time: {reply.RoundtripTime} ms\n");
                    }
                    else
                    {
                        Console.WriteLine($"Ping failed: {reply.Status}");
                    }
                }
            }
            catch (PingException pe)
            {
                Console.WriteLine($"Ping failed: {pe.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
    }
}
