namespace LoanBook.Collections.Endpoint.EventHandlers
{
    using System;

    using LoanBook.PaymentGateway.Messaging.Events;

    using NServiceBus;

    public class PaymentTakenHandler : IHandleMessages<PaymentTaken>
    {
        public void Handle(PaymentTaken message)
        {
            Console.WriteLine("payment taken");
        }
    }
}
