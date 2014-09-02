using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;

namespace LoanBook.Origination.Messaging.Commands
{
    public class CreditCheckApplication : ICommand
    {
        public Guid ApplicationId { get; set; }

        public string Topic
        {
            get { return TopicNames.OriginationCommands; }
        }
    }
}
