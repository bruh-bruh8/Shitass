using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class CdCommand : ICommand
    {
        public string Name => "cd";
        public string[] Aliases => new string[] { "chdir" };
        public string Description => "Change current directory";
        public string Usage => "cd <path>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(Environment.CurrentDirectory);
                return Task.CompletedTask;
            }

            string path = string.Join(" ", args.Skip(1));

            path = ResolveShortcut(path);

            try
            {
                if (!Path.IsPathRooted(path))
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, path));
                }

                if (Directory.Exists(path))
                {
                    Environment.CurrentDirectory = path;

                    string newTitle = $"{context.Settings.WindowTitle} | {path}";
                    Program.UpdateTitle(newTitle);
                }
                else
                {
                    Console.WriteLine("Directory not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return Task.CompletedTask;
        }

        private string ResolveShortcut(string path)
        {
            string user = Environment.UserName;

            switch (path.ToLower())
            {
                case "downloads":
                    return $@"C:\Users\{user}\Downloads";
                case "documents":
                    return $@"C:\Users\{user}\Documents";
                case "desktop":
                    return $@"C:\Users\{user}\Desktop";
                case "pictures":
                    return $@"C:\Users\{user}\Pictures";
                case "music":
                    return $@"C:\Users\{user}\Music";
                case "videos":
                    return $@"C:\Users\{user}\Videos";
                case "home":
                case "~":
                    return $@"C:\Users\{user}";
                case "appdata":
                    return $@"C:\Users\{user}\AppData\Roaming";
                case "localappdata":
                    return $@"C:\Users\{user}\AppData\Local";
                case "temp":
                    return Path.GetTempPath();
                default:
                    return path;
            }
        }
    }
}