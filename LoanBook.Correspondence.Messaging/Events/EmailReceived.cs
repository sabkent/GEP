using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Correspondence.Messaging.Events
{
    public sealed class EmailReceived
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public DateTime Received { get; set; }
    }
}
