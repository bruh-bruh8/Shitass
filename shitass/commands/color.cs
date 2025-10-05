using System;
using System.IO;
using System.Threading.Tasks;

namespace shitass
{
    public class ColorCommand : ICommand
    {
        public string Name => "color";
        public string[] Aliases => new string[] { };
        public string Description => "Changes the color of the console background/text";
        public string Usage => "color background/text <color> OR color -reset OR color preset <name>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: color background/text <color> OR color -reset");
                Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                return Task.CompletedTask;
            }

            string property = args[1].ToLower();
            string color = args.Length > 2 ? args[2] : "";

            try
            {
                string[] lines = File.ReadAllLines(context.SettingsPath);

                if (property == "-reset")
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    lines[2] = "bgcolor=black";
                    lines[3] = "fgcolor=white";
                    File.WriteAllLines(context.SettingsPath, lines);
                    Console.Clear();
                    Console.WriteLine("shitass\n");
                    Console.WriteLine("Reset console colors");
                    return Task.CompletedTask;
                }

                switch (property)
                {
                    case "foreground":
                    case "fg":
                    case "text":
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                        Console.WriteLine($"Changed foreground color to {color}");
                        lines[3] = "fgcolor=" + color;
                        File.WriteAllLines(context.SettingsPath, lines);
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        break;

                    case "background":
                    case "bg":
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                        lines[2] = "bgcolor=" + color;
                        File.WriteAllLines(context.SettingsPath, lines);
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        Console.WriteLine($"Changed background color to {color}\n");
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        break;

                    case "preset":
                        switch (color)
                        {
                            case "light": // todo: make console self destruct if selected
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                lines[2] = "bgcolor=white";
                                lines[3] = "fgcolor=black";
                                File.WriteAllLines(context.SettingsPath, lines);
                                Console.Clear();
                                Console.WriteLine("shitass\n");
                                break;

                            case "dark":
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                lines[2] = "bgcolor=black";
                                lines[3] = "fgcolor=white";
                                File.WriteAllLines(context.SettingsPath, lines);
                                Console.Clear();
                                Console.WriteLine("shitass\n");
                                break;

                            case "hackerman":
                            case "skid":
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Green;
                                lines[2] = "bgcolor=black";
                                lines[3] = "fgcolor=green";
                                File.WriteAllLines(context.SettingsPath, lines);
                                Console.Clear();
                                Console.WriteLine("shitass\n");
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid property, use 'text' or 'background'.");
                        break;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Error: Invalid color");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
