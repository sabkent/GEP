using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.CustomerRelationshipManagement.Messaging.Commands
{
    public class CheckDuplication
    {
        public Guid ApplicationId { get; set; }
    }
}
