using System;

namespace LoanBook.PaymentGateway.Core
{
    public class CardHolder
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address BillingAddress { get; set; }
        public Card PaymentCard{ get; set; }
    }
}
