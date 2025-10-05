using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace shitass
{
    public class CommandContext
    {
        public string User { get; set; }
        public string SettingsPath { get; set; }
        // Add other shared state here
    }
    public interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }
        string Description { get; }
        string Usage { get; }
        Task ExecuteAsync(string[] args, CommandContext context);
    }
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public void RegisterCommand(ICommand command)
        {
            _commands[command.Name.ToLower()] = command;
            foreach (var alias in command.Aliases)
            {
                _commands[alias.ToLower()] = command;
            }
        }

        public void AutoRegisterCommands()
        {
            var commandType = typeof(ICommand);
            var commands = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => commandType.IsAssignableFrom(t)
                 && !t.IsInterface
                 && !t.IsAbstract
                 && t != typeof(HelpCommand))  // ← Add this line
        .Select(t => (ICommand)Activator.CreateInstance(t));

            foreach (var cmd in commands)
            {
                RegisterCommand(cmd);
            }
        }

        public ICommand GetCommand(string name)
        {
            _commands.TryGetValue(name.ToLower(), out var command);
            return command;
        }

        public IEnumerable<ICommand> GetAllCommands()
        {
            return _commands.Values.Distinct();
        }
    }
}
