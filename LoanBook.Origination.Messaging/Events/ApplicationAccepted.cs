using System;

namespace LoanBook.Origination.Messaging.Events
{
    public sealed class ApplicationAccepted
    {
        public Guid ApplicationId { get; set; }

        public Guid ApplicantId { get; set; }

        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
    }
}
