using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class ProcessCommand : ICommand
    {
        public string Name => "ps";
        public string[] Aliases => new string[] { "proc", "process" };
        public string Description => "List or kill processes";
        public string Usage => "ps list [filter] OR ps kill <name|pid>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ps list [filter] OR ps kill <name|pid>");
                return Task.CompletedTask;
            }

            string action = args[1].ToLower();

            try
            {
                if (action == "list" || action == "ls")
                {
                    string filter = args.Length > 2 ? args[2].ToLower() : "";
                    var processes = Process.GetProcesses()
                        .Where(p => filter == "" || p.ProcessName.ToLower().Contains(filter))
                        .OrderByDescending(p => p.WorkingSet64)
                        .Take(20);

                    Console.WriteLine($"\n{"PID",-8} {"Name",-30} {"Memory (MB)",-15}");
                    Console.WriteLine(new string('-', 55));

                    foreach (var proc in processes)
                    {
                        try
                        {
                            double memoryMB = proc.WorkingSet64 / (1024.0 * 1024.0);
                            Console.WriteLine($"{proc.Id,-8} {proc.ProcessName,-30} {memoryMB,-15:F2}");
                        }
                        catch { } // some processes cant be accessed
                    }
                    Console.WriteLine();
                }
                else if (action == "kill")
                {
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: ps kill <name|pid>");
                        return Task.CompletedTask;
                    }

                    string target = args[2];

                    if (int.TryParse(target, out int pid))
                    {
                        // pid
                        var proc = Process.GetProcessById(pid);

                        if (IsProtectedProcess(proc.ProcessName))
                        {
                            if (!ConfirmDangerousKill(proc.ProcessName))
                            {
                                Console.WriteLine("Kill cancelled.");
                                return Task.CompletedTask;
                            }
                        }

                        proc.Kill();
                        Console.WriteLine($"Killed process {pid}");
                    }
                    else
                    {
                        // name
                        if (IsProtectedProcess(target))
                        {
                            if (!ConfirmDangerousKill(target))
                            {
                                Console.WriteLine("Kill cancelled.");
                                return Task.CompletedTask;
                            }
                        }

                        var processes = Process.GetProcessesByName(target);
                        if (processes.Length == 0)
                        {
                            Console.WriteLine($"No process found with name: {target}");
                        }
                        else
                        {
                            foreach (var proc in processes)
                            {
                                proc.Kill();
                                Console.WriteLine($"Killed {proc.ProcessName} (PID: {proc.Id})");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }

        private bool IsProtectedProcess(string processName)
        {
            string[] protectedProcesses = { "explorer", "shitass" };
            return protectedProcesses.Any(p => processName.ToLower().Contains(p));
        }

        private bool ConfirmDangerousKill(string processName)
        {
            if (processName.ToLower().Contains("explorer"))
            {
                Console.WriteLine("\nWARNING");
                Console.WriteLine("Killing explorer.exe probably isn't a good idea.");
            }
            else if (processName.ToLower().Contains("shitass"))
            {
                Console.WriteLine("\n??? tf did i do");
            }

            Console.Write("\nType 'yes' to confirm: ");
            string response = Console.ReadLine();
            return response?.ToLower() == "yes";
        }
    }
}