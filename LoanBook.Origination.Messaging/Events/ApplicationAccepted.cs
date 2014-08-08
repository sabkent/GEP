using System;
using LoanBook.Messaging;

namespace LoanBook.Origination.Messaging.Events
{
    public sealed class ApplicationAccepted : IEvent
    {
        public Guid ApplicationId { get; set; }

        public Guid ApplicantId { get; set; }

        public decimal Amount { get; set; }
        public decimal Interest { get; set; }

        public DateTime FirstInstallment { get; set; }

        public int InstallmentCount { get; set; }

        public string Topic
        {
            get { return TopicNames.OriginationEvents; }
        }
    }
}
