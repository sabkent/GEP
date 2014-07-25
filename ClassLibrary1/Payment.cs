using System;

namespace ClassLibrary1
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime Received { get; set; }
        public decimal Amount { get; set; }
    }
}
