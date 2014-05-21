using System;

namespace LoanBook.DepositGateway.Messaging.Events
{
    public sealed class DepositSucceeded
    {
        public DepositSucceeded(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
