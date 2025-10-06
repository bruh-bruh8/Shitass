using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shitass
{
    public class SettingsCommand : ICommand
    {
        public string Name => "settings";
        public string[] Aliases => new string[] { "config", "set", "setting" };
        public string Description => "View or change settings";
        public string Usage => "settings [list|get|set] [key] [value]";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                ListSettings(context.Settings, context.User);
                return Task.CompletedTask;
            }

            string action = args[1].ToLower();

            switch (action)
            {
                case "list":
                case "ls":
                    ListSettings(context.Settings, context.User);
                    break;

                case "get":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: settings get <key>");
                        break;
                    }
                    GetSetting(args[2], context.Settings);
                    break;

                case "set":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: settings set <key> [value]");
                        break;
                    }
                    string key = args[2];
                    string value = args.Length > 3 ? string.Join(" ", args.Skip(3)) : "";
                    SetSetting(key, value, context);
                    break;

                default:
                    Console.WriteLine("Invalid action. Use 'list', 'get', or 'set'.");
                    break;
            }

            return Task.CompletedTask;
        }
        private void ListSettings(Settings settings, string user)
        {
            Console.WriteLine("\nCurrent Settings:");
            Console.WriteLine($"  title: {settings.WindowTitle}");
            Console.WriteLine($"  bgcolor: {settings.BackgroundColor}");
            Console.WriteLine($"  fgcolor: {settings.ForegroundColor}");
            Console.WriteLine($"  promptname: {(string.IsNullOrWhiteSpace(settings.PromptName) ? user : settings.PromptName)}");
            Console.WriteLine($"  promptsymbol: {settings.PromptSymbol}");
            Console.WriteLine($"  showtimeintitle: {settings.ShowTimeInTitle}");
            Console.WriteLine($"  titledateformat: {settings.TitleDateFormat}");
            Console.WriteLine($"  showstartupmessage: {settings.ShowStartupMessage}");
            Console.WriteLine($"  autocheckupdates: {settings.AutoCheckUpdates}");
            Console.WriteLine();
        }

        private void GetSetting(string key, Settings settings)
        {
            switch (key.ToLower())
            {
                case "promptname":
                    Console.WriteLine(string.IsNullOrWhiteSpace(settings.PromptName) ? "(using real username)" : settings.PromptName);
                    break;
                case "promptsymbol":
                    Console.WriteLine(settings.PromptSymbol);
                    break;
                case "showtimeintitle":
                    Console.WriteLine(settings.ShowTimeInTitle);
                    break;
                case "titledateformat":
                    Console.WriteLine(settings.TitleDateFormat);
                    break;
                case "showstartupmessage":
                    Console.WriteLine(settings.ShowStartupMessage);
                    break;
                case "autocheckupdates":
                    Console.WriteLine(settings.AutoCheckUpdates);
                    break;
                default:
                    Console.WriteLine($"Unknown setting: {key}");
                    break;
            }
        }

        private void SetSetting(string key, string value, CommandContext context)
        {
            try
            {
                switch (key.ToLower())
                {
                    case "wintitle":
                    case "windowtitle":
                    case "title":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current window title: " + context.Settings.WindowTitle);
                            Console.WriteLine("Usage: settings set title <new title>");
                            break;
                        }
                        context.Settings.WindowTitle = value;
                        context.Settings.Save(context.SettingsPath);
                        Program.UpdateTitle(value);
                        Console.WriteLine($"Set window title to: {value}");
                        break;

                    case "bgcolor":
                    case "background":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current background color: " + context.Settings.BackgroundColor);
                            Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                            break;
                        }
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value, true);
                        context.Settings.BackgroundColor = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.Clear();
                        Console.WriteLine("shitass\n");
                        Console.WriteLine($"Set background color to: {value}");
                        break;

                    case "fgcolor":
                    case "foreground":
                    case "textcolor":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current text color: " + context.Settings.ForegroundColor);
                            Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                            break;
                        }
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value, true);
                        context.Settings.ForegroundColor = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set text color to: {value}");
                        break;

                    case "promptname":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current prompt name: " + (string.IsNullOrWhiteSpace(context.Settings.PromptName) ? context.User : context.Settings.PromptName));
                            Console.WriteLine("Usage: settings set promptname <name> (leave empty to use your username)");
                            break;
                        }
                        context.Settings.PromptName = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set prompt name to: {value}");
                        break;

                    case "promptsymbol":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current prompt symbol: " + context.Settings.PromptSymbol);
                            Console.WriteLine("Usage: settings set promptsymbol <symbol> (e.g., >, $, #)");
                            break;
                        }
                        context.Settings.PromptSymbol = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set prompt symbol to: {value}");
                        break;

                    case "titledateformat":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            Console.WriteLine("Current format: " + context.Settings.TitleDateFormat);
                            Console.WriteLine("Available formats: full, time, date, short");
                            Console.WriteLine("  full  - Complete date and time");
                            Console.WriteLine("  time  - Time only (HH:mm:ss)");
                            Console.WriteLine("  date  - Date only (yyyy-MM-dd)");
                            Console.WriteLine("  short - Short time (HH:mm)");
                            break;
                        }
                        context.Settings.TitleDateFormat = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set title date format to: {value}");
                        break;

                    case "showtimeintitle":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            context.Settings.ShowTimeInTitle = !context.Settings.ShowTimeInTitle;
                        }
                        else
                        {
                            context.Settings.ShowTimeInTitle = value.ToLower() == "true";
                        }
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Show time in title: {context.Settings.ShowTimeInTitle}");
                        break;

                    case "showstartupmessage":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            context.Settings.ShowStartupMessage = !context.Settings.ShowStartupMessage;
                        }
                        else
                        {
                            context.Settings.ShowStartupMessage = value.ToLower() == "true";
                        }
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Show startup message: {context.Settings.ShowStartupMessage}");
                        break;

                    case "autocheckupdates":
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            context.Settings.AutoCheckUpdates = !context.Settings.AutoCheckUpdates;
                        }
                        else
                        {
                            context.Settings.AutoCheckUpdates = value.ToLower() == "true";
                        }
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Auto check updates: {context.Settings.AutoCheckUpdates}");
                        break;

                    default:
                        Console.WriteLine($"Unknown setting: {key}");
                        Console.WriteLine("Use 'settings list' to see all available settings.");
                        break;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Error: Invalid value");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
