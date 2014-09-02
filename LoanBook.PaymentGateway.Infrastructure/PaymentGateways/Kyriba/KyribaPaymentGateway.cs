using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.PaymentGateway.Core;

namespace LoanBook.PaymentGateway.Infrastructure.PaymentGateways.Kyriba
{
    public sealed class KyribaPaymentGateway : IMakePaymentProvider
    {
        public MakePaymentResponse MakePayment(MakePaymentRequest makePaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
