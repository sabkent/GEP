using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Collections.Core.Query.Projections
{
    public class DuePayment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
