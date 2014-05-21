using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Origination.Messaging.Commands
{
    public class SubmitApplication
    {
        public Guid ApplicationId { get; set; }
    }
}
