using System;

namespace ClassLibrary1.Collections
{
    public class Debt
    {
        public Guid Id { get; set; }
        public DateTime Due { get; set; }
        public decimal Amount { get; set; }
        public int Attempt { get; set; }
    }
}