using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Collections.Core.Query.Projections;

namespace LoanBook.Collections.Core.Query
{
    public interface IQueryPayments
    {
        IEnumerable<DuePayment> GetPaymentDueFor(DateTime dueDate);
    }
}
