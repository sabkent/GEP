using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.DepositGateway.Messaging.Events
{
    public sealed class DepositScheduled
    {
        public DepositScheduled(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
