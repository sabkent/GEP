using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Fraud.Messaging.Events
{
    public sealed class FraudChecksHaveBeenRun
    {
        public Guid ApplicationId { get; set; }
    }
}
