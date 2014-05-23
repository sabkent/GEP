namespace LoanBook.Fraud.Endpoint.CommandHandlers
{
    using System;
    using LoanBook.Fraud.Messaging.Commands;
    using LoanBook.Fraud.Messaging.Events;

    using NServiceBus;

    class RunFraudChecksHandler : IHandleMessages<RunFraudChecks>
    {
        private readonly IBus bus;

        public RunFraudChecksHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(RunFraudChecks runFraudChecks)
        {
            Console.WriteLine("run fraud checks");
            this.bus.Publish(new FraudChecksHaveBeenRun{ApplicationId = runFraudChecks.ApplicationId});
        }
    }
}
