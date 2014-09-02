using System;
namespace LoanBook.Collections.Core.Entities
{
    public class Debt
    {
        public Guid Id { get; set; }
        public Guid DebtorId { get; set; }
        public decimal Amount { get; set; }

        public DateTime Due { get; set; }
    }
}
