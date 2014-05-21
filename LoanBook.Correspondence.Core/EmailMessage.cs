using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Correspondence.Core
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string To { get; set; }

        public DateTime Received { get; set; }
    }
}
