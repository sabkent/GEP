namespace LoanBook.DepositGateway.Messaging.Commands
{
    using System;

    public sealed class ScheduleDeposit
    {
        public ScheduleDeposit(Guid id, string accountNumber, string sortCode, DateTime date)
        {
            Id = id;
            AccountNumber = accountNumber;
            SortCode = sortCode;
            Date = date;
        }

        public Guid Id { get; private set; }
        public string AccountNumber { get; private set; }
        public string SortCode { get; private set; }
        public DateTime Date { get; private set; }
    }
}
