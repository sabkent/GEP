using System;
using System.Collections.Generic;
using LoanBook.Endpoint;
using LoanBook.PaymentGateway.Messaging.Events;
using NServiceBus;

namespace LoanBook.PaymentGateway.Endpoint
{
    class CommandSimulator : IWantToRunWhenBusStartsAndStops
    {
        private IBus _bus;

        public CommandSimulator(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            new CommandInterpreter(_bus, new Dictionary<string, Action<IBus, string>>
            {
                {"fail", SendFailedPaymentEvent}
            }).Run();
        }

        public void Stop()
        {
            
        }

        private void SendFailedPaymentEvent(IBus bus, string args)
        {
            Guid id;
            if (Guid.TryParse(args, out id))
                bus.Publish(new PaymentFailed{Id = id});
        }
    }
}
