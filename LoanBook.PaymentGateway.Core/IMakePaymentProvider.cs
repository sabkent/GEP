﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.PaymentGateway.Core
{
    public interface IMakePaymentProvider
    {
        MakePaymentResponse MakePayment(MakePaymentRequest makePaymentRequest);
    }
}
