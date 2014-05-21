using System;
using System.Collections.Generic;
using NServiceBus;

namespace LoanBook.Endpoint
{
    public class CommandInterpreter
    {
        private IBus _bus;
        private Dictionary<string, Action<IBus, string>> _commands;

        public CommandInterpreter(IBus bus, Dictionary<string, Action<IBus, string>> commands)
        {
            _bus = bus;
            _commands = commands;
        }

        public void Run()
        {
            while (true)
            {
                var command = Console.ReadLine();
                var commandName = command.Substring(0, command.IndexOf(' '));

                var args = command.Replace(commandName, String.Empty).Trim();

                Action<IBus, string> action;
                if (_commands.TryGetValue(commandName, out action))
                    action(_bus, args);
            }
        }
    }
}
