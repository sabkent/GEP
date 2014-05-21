namespace LoanBook.Financial.Messaging.Events
{
    using System;

    public sealed class AccountInArrears
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
    }
}
