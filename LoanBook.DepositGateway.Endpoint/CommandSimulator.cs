using System.Collections.Generic;
using LoanBook.DepositGateway.Messaging.Events;

namespace LoanBook.DepositGateway.Endpoint
{
    using System;
    using NServiceBus;
    using LoanBook.Endpoint;

    class CommandSimulator : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus _bus;

        public CommandSimulator(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            new CommandInterpreter(_bus, new Dictionary<string, Action<IBus, string>>
            {
                {"succeed", SendDepositSuccedded},
                
            }).Run();
        }

        public void Stop()
        {
            
        }

        private void SendDepositSuccedded(IBus bus, string args)
        {
            Guid id;
            if (Guid.TryParse(args, out id)) 
                bus.Publish(new DepositSucceeded(id));
        }
    }
}
