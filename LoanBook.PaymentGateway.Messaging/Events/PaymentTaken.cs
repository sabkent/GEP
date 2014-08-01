using System;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Messaging.Events
{
    public class PaymentTaken : IEvent
    {
        public Guid CorrelationId { get; set; }
        public string ProviderReference { get; set; }
        public bool Succeeded { get; set; }

        public string Topic
        {
            get { return TopicNames.PaymentGatewayEvents; }
        }
    }
}
