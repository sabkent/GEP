using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Collections.Core.Data
{
    public interface IPaymentRepository
    {
        void Add(Payment payment);
    }
}
