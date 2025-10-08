using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    class TitleCommand : ICommand
    {
        public string Name => "title";
        public string[] Aliases => new string[] { "windowtitle", "settitle", "setwindowtitle" };
        public string Description => "Changes the title of the window";
        public string Usage => "title ";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args[1] == "-reset")
            {
                string[] lines = File.ReadAllLines(context.SettingsPath);
                lines[1] = "wintitle=shitass";
                File.WriteAllLines(context.SettingsPath, lines);
                Program.UpdateTitle("shitass");
                Console.WriteLine("Reset window title");
                return Task.CompletedTask;
            }
            else
            {
                string newTitle = string.Join(" ", args.Skip(1));

                string[] lines = File.ReadAllLines(context.SettingsPath);
                lines[1] = "wintitle=" + newTitle;
                File.WriteAllLines(context.SettingsPath, lines);

                Program.UpdateTitle(newTitle);

                Console.WriteLine($"Window title changed to: {newTitle}");
            }
            return Task.CompletedTask;
        }
    }
}
