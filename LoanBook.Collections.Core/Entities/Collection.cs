using System;

namespace LoanBook.Collections.Core.Entities
{
    public class Collection
    {
        public Guid DebtId { get; set; }
        public string Reference { get; set; }
        public bool Succedded { get; set; }
    }
}