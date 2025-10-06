using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace shitass.commands
{
    public class PortCommand : ICommand
    {
        public string Name => "port";
        public string[] Aliases => new string[] { };
        public string Description => "Check what's using a specific port";
        public string Usage => "port <port number>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int port))
            {
                Console.WriteLine("Usage: port <port number>");
                return Task.CompletedTask;
            }

            var props = IPGlobalProperties.GetIPGlobalProperties();
            var listeners = props.GetActiveTcpListeners();
            var connections = props.GetActiveTcpConnections();

            bool found = false;

            foreach (var listener in listeners.Where(l => l.Port == port))
            {
                Console.WriteLine($"\nPort {port} is being listened on by a process");
                Console.WriteLine($"Address: {listener.Address}");
                found = true;
            }

            foreach (var conn in connections.Where(c => c.LocalEndPoint.Port == port || c.RemoteEndPoint.Port == port))
            {
                Console.WriteLine($"\nPort {port} active connection:");
                Console.WriteLine($"Local: {conn.LocalEndPoint}");
                Console.WriteLine($"Remote: {conn.RemoteEndPoint}");
                Console.WriteLine($"State: {conn.State}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine($"\nPort {port} is not in use");
            }

            Console.WriteLine();
            return Task.CompletedTask;
        }
    }
}
