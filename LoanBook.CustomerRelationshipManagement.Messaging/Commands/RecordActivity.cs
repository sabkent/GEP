using LoanBook.Messaging;

namespace LoanBook.CustomerRelationshipManagement.Messaging.Commands
{
    public class RecordActivity : ICommand
    {
        public string Topic
        {
            get { return TopicNames.CustomerRelationshipManagementCommands; }
        }
    }
}