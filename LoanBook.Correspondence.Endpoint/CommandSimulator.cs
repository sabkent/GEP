namespace LoanBook.Correspondence.Endpoint
{
    using System;
    using System.Collections.Generic;

    using LoanBook.Correspondence.Messaging.Commands;
    using LoanBook.Endpoint;

    using NServiceBus;

    class CommandSimulator : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus bus;

        public CommandSimulator(IBus bus)
        {
            this.bus = bus;
        }

        public void Start()
        {
            new CommandInterpreter(bus, new Dictionary<string, Action<IBus, string>>
            {
                {"checkemail", SendCheckEmailCommand},
            }).Run();
        }

        public void Stop()
        {
            
        }

        private void SendCheckEmailCommand(IBus bus, string args)
        {
            bus.Send(new CheckEmail());
        }
    }
}
