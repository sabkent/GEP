using System;

namespace LoanBook.Collections.Endpoint.CommandHandlers
{
    using LoanBook.Collections.Messaging.Commands;
    using NServiceBus;

    class SchedulePaymentPlanCommandHandler : IHandleMessages<SchedulePaymentPlan>
    {
        public void Handle(SchedulePaymentPlan message)
        {
            Console.WriteLine("payment plan scheduled");
        }
    }
}
