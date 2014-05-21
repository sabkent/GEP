using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Financial.Messaging.Events
{
    public sealed class AccountSettled
    {
        public AccountSettled(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
