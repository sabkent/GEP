using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Origination.Core
{
    public class Application
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
