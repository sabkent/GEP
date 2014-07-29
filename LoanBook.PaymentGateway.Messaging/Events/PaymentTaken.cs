using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Messaging.Events
{
    public class PaymentTaken : IEvent
    {
        public Guid Reference { get; set; }

        public string Topic
        {
            get { return TopicNames.PaymentGatewayEvents; }
        }
    }
}
