using LoanBook.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Origination.Messaging.Commands
{
    public class SubmitApplication : ICommand
    {
        public Guid ApplicationId { get; set; }

        public string Topic
        {
            get { return TopicNames.OriginationCommands; }
        }
    }
}
