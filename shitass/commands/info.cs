using System;
using System.Threading.Tasks;

namespace shitass
{
    class InfoCommand : ICommand
    {
        public string Name => "info";
        public string[] Aliases => new[] { "cmd", "shitass", "version" };
        public string Description => "Info about the console";
        public string Usage => "info";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            Console.WriteLine("         __    _ __                 \n" +
                        "   _____/ /_  (_) /_____ ___________\n" +
                        "  / ___/ __ \\/ / __/ __ `/ ___/ ___/\n" +
                        " (__  ) / / / / /_/ /_/ (__  |__  ) \n" +
                        "/____/_/ /_/_/\\__/\\__,_/____/____/  \n" +
                        "                                    \n" +
                        "Shitass b" + Program.version +
                            ", made by orange\n");
            return Task.CompletedTask;
        }
    }
}
