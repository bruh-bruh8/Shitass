using System;
using System.Linq;
using System.Threading.Tasks;

namespace shitass
{
    public class SettingsCommand : ICommand
    {
        public string Name => "settings";
        public string[] Aliases => new string[] { "config", "set", "setting" };
        public string Description => "View or change settings";
        public string Usage => "settings [key] [value]";

        private static readonly string[] BooleanSettings = { "showtimeintitle", "showstartupmessage", "autocheckupdates" };

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                // formerly list/no args
                ListSettings(context.Settings, context.User);
                return Task.CompletedTask;
            }

            string key = args[1].ToLower();

            if (args.Length < 3) // no more set
            {
                if (IsBooleanSetting(key))
                {
                    ToggleSetting(key, context);
                }
                else
                {
                    ShowSetting(key, context);
                }
            }
            else
            {
                string value = string.Join(" ", args.Skip(2));
                SetSetting(key, value, context);
            }

            return Task.CompletedTask;
        }

        private bool IsBooleanSetting(string key)
        {
            return BooleanSettings.Contains(key);
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

        private void ShowSetting(string key, CommandContext context)
        {
            switch (key)
            {
                case "wintitle":
                case "windowtitle":
                case "title":
                    Console.WriteLine("Current window title: " + context.Settings.WindowTitle);
                    Console.WriteLine("Usage: settings title <new title>");
                    break;

                case "bgcolor":
                case "background":
                    Console.WriteLine("Current background color: " + context.Settings.BackgroundColor);
                    Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                    break;

                case "fgcolor":
                case "foreground":
                case "textcolor":
                    Console.WriteLine("Current text color: " + context.Settings.ForegroundColor);
                    Console.WriteLine("Available colors: Black, White, Gray, DarkGray, Red, DarkRed, Blue, DarkBlue, Green, DarkGreen, Yellow, DarkYellow, Cyan, DarkCyan, Magenta, DarkMagenta");
                    break;

                case "promptname":
                    Console.WriteLine("Current prompt name: " + (string.IsNullOrWhiteSpace(context.Settings.PromptName) ? context.User : context.Settings.PromptName));
                    Console.WriteLine("Usage: settings promptname <name>");
                    break;

                case "promptsymbol":
                    Console.WriteLine("Current prompt symbol: " + context.Settings.PromptSymbol);
                    Console.WriteLine("Usage: settings promptsymbol <symbol> (e.g., >, $, #)");
                    break;

                case "titledateformat":
                    Console.WriteLine("Current format: " + context.Settings.TitleDateFormat);
                    Console.WriteLine("Available formats: full, time, date, short");
                    Console.WriteLine("  full  - Complete date and time");
                    Console.WriteLine("  time  - Time only (HH:mm:ss)");
                    Console.WriteLine("  date  - Date only (yyyy-MM-dd)");
                    Console.WriteLine("  short - Short time (HH:mm)");
                    break;

                default:
                    Console.WriteLine($"Unknown setting: {key}");
                    Console.WriteLine("Use 'settings' to see all available settings.");
                    break;
            }
        }

        private void ToggleSetting(string key, CommandContext context)
        {
            switch (key)
            {
                case "showtimeintitle":
                    context.Settings.ShowTimeInTitle = !context.Settings.ShowTimeInTitle;
                    context.Settings.Save(context.SettingsPath);
                    Console.WriteLine($"Show time in title: {context.Settings.ShowTimeInTitle}");
                    break;

                case "showstartupmessage":
                    context.Settings.ShowStartupMessage = !context.Settings.ShowStartupMessage;
                    context.Settings.Save(context.SettingsPath);
                    Console.WriteLine($"Show startup message: {context.Settings.ShowStartupMessage}");
                    break;

                case "autocheckupdates":
                    context.Settings.AutoCheckUpdates = !context.Settings.AutoCheckUpdates;
                    context.Settings.Save(context.SettingsPath);
                    Console.WriteLine($"Auto check updates: {context.Settings.AutoCheckUpdates}");
                    break;
            }
        }

        private void SetSetting(string key, string value, CommandContext context)
        {
            try
            {
                switch (key)
                {
                    case "wintitle":
                    case "windowtitle":
                    case "title":
                        context.Settings.WindowTitle = value;
                        context.Settings.Save(context.SettingsPath);
                        Program.UpdateTitle(value);
                        Console.WriteLine($"Set window title to: {value}");
                        break;

                    case "bgcolor":
                    case "background":
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value, true);
                        context.Settings.BackgroundColor = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.Clear();
                        if (context.Settings.ShowStartupMessage)
                        {
                            Console.WriteLine("shitass\n");
                        }
                        Console.WriteLine($"Set background color to: {value}");
                        break;

                    case "fgcolor":
                    case "foreground":
                    case "textcolor":
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), value, true);
                        context.Settings.ForegroundColor = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set text color to: {value}");
                        break;

                    case "promptname":
                        context.Settings.PromptName = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set prompt name to: {value}");
                        break;

                    case "promptsymbol":
                        context.Settings.PromptSymbol = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set prompt symbol to: {value}");
                        break;

                    case "titledateformat":
                        context.Settings.TitleDateFormat = value;
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Set title date format to: {value}");
                        break;

                    case "showtimeintitle":
                        context.Settings.ShowTimeInTitle = value.ToLower() == "true";
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Show time in title: {context.Settings.ShowTimeInTitle}");
                        break;

                    case "showstartupmessage":
                        context.Settings.ShowStartupMessage = value.ToLower() == "true";
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Show startup message: {context.Settings.ShowStartupMessage}");
                        break;

                    case "autocheckupdates":
                        context.Settings.AutoCheckUpdates = value.ToLower() == "true";
                        context.Settings.Save(context.SettingsPath);
                        Console.WriteLine($"Auto check updates: {context.Settings.AutoCheckUpdates}");
                        break;

                    default:
                        Console.WriteLine($"Unknown setting: {key}");
                        Console.WriteLine("Use 'settings' to see all available settings.");
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