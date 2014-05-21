using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.PaymentGateway.Endpoint.CommandHandlers
{
    using LoanBook.PaymentGateway.Messaging.Commands;
    using LoanBook.PaymentGateway.Messaging.Events;

    using NServiceBus;

    class TakePaymentCommandHandler : IHandleMessages<TakePayment>
    {
        private readonly IBus bus;

        public TakePaymentCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(TakePayment message)
        {
            Console.WriteLine("Take payment handler invoked");
            this.bus.Publish(new PaymentTaken());
        }
    }
}
