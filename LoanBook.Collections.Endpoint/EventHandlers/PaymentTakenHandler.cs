using LoanBook.Messaging;

namespace LoanBook.Collections.Endpoint.EventHandlers
{
    using System;

    using LoanBook.PaymentGateway.Messaging.Events;

    public class PaymentTakenHandler : ISubscribeToEvent<PaymentTaken>
    {
        public void Handle(PaymentTaken message)
        {
            Console.WriteLine("payment taken");
        }

        public void Notify(PaymentTaken @event)
        {
            throw new NotImplementedException();
        }
    }
}
