
namespace LoanBook.PaymentGateway.Core
{
    public class DirectPaymentRequest
    {
        public CardHolder CardHolder { get; set; }
        public decimal Amount { get; set; }
    }
}