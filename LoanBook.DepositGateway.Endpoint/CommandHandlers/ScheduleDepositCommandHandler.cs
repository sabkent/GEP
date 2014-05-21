using LoanBook.DepositGateway.Messaging.Events;

namespace LoanBook.DepositGateway.Endpoint.CommandHandlers
{
    using System;
    using Messaging.Commands;
    using NServiceBus;

    internal sealed class ScheduleDepositCommandHandler : IHandleMessages<ScheduleDeposit>
    {
        private readonly IBus _bus;

        public ScheduleDepositCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ScheduleDeposit scheduleDeposit)
        {
            _bus.Publish(new DepositScheduled(scheduleDeposit.Id));
        }
    }
}
