using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Fraud.Endpoint.CommandHandlers
{
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
