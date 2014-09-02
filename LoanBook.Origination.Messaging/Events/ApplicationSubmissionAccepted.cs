using System;
using LoanBook.Messaging;

namespace LoanBook.Origination.Messaging.Events
{
    public class ApplicationSubmissionAccepted : IEvent
    {
        public Guid ApplicationId { get; set; }

        public string Topic
        {
            get { return TopicNames.OriginationEvents; }
        }
    }
}
