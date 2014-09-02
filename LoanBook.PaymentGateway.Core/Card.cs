using System;

namespace LoanBook.PaymentGateway.Core
{
    public class Card
    {
        public string Number { get; set; }

        public string Cvv2 { get; set; }
        public DateTime Expires { get; set; }
        public string Type { get; set; }
    }
}
