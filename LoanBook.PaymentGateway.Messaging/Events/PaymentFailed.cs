using System;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Messaging.Events
{
    public sealed class PaymentFailed : IEvent
    {
        public Guid CorrelationId { get; set; }

        public string Topic
        {
            get { return TopicNames.PaymentGatewayEvents; }
        }
    }
}
