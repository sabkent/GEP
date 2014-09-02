using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Messaging.Events
{
    public sealed class CardSavedForApplication : IEvent
    {
        public Guid ApplicationId { get; set; }

        public string Topic
        {
            get { return TopicNames.PaymentGatewayEvents; }
        }
    }
}
