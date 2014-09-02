using System;

namespace LoanBook.LoanManagementSystem.Models
{
    public class ApplicationSubmission
    {
        public decimal Amount { get; set; }

        public string CorrelationId { get; set; }

        public bool Accepted { get; set; }
    }
}