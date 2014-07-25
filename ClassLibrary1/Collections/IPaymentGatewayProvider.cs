using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Collections
{
    public interface IPaymentGatewayProvider
    {
        Task<TakePaymentResponse> TakePayment(TakePaymentRequest takePaymentRequest);
    }

    public class TakePaymentRequest
    {
        public Guid CardToken{ get; set; }
        public decimal Amount { get; set; }
    }

    public class TakePaymentResponse
    {
        public string Reference { get; set; }
        public bool IsSuccess { get; set; }
    }
}
