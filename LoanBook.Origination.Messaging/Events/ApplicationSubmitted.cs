using System;
using LoanBook.Messaging;

namespace LoanBook.Origination.Messaging.Events
{
    public sealed class ApplicationSubmitted : IEvent
    {
        public Guid ApplicationId { get; set; }

        public string Topic
        {
            get { return TopicNames.OriginationEvents; }
        }
    }
}
