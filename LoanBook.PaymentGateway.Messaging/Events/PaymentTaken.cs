using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.PaymentGateway.Messaging.Events
{
    public class PaymentTaken
    {
        public Guid Reference { get; set; }
    }
}
