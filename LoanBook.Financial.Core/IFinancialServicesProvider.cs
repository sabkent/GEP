using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Financial.Core.Dto;

namespace LoanBook.Financial.Core
{
    public interface IFinancialServicesProvider
    {
        void DirectLoanPayment(DirectLoanPaymentRequest directLoanPaymentRequest);
    }
}
