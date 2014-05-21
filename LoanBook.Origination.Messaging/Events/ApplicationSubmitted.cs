using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Origination.Messaging.Events
{
    public sealed class ApplicationSubmitted
    {
        public Guid ApplicationId { get; set; }
    }
}
