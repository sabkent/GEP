namespace LoanBook.PaymentGateway.Messaging.Events
{
    using System;

    public sealed class PaymentFailed
    {
        public Guid Id { get; set; }
    }
}
