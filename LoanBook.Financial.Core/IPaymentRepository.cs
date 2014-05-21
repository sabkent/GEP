using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Financial.Core
{
    public interface IPaymentRepository
    {
        void Add(Payment payment);
    }
}
