namespace LoanBook.PaymentGateway.Endpoint
{
    using System;
    using LoanBook.PaymentGateway.Messaging.Events;

    using NServiceBus;

    class PaymentScheduleMocker : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus bus;

        public PaymentScheduleMocker(IBus bus)
        {
            this.bus = bus;
        }

        public void Start()
        {
            //Schedule.Every(TimeSpan.FromSeconds(10)).Action(
            //    () => this.bus.Publish(new PaymentTaken()));
        }

        public void Stop()
        {
            
        }
    }
}
