using LoanBook.Messaging;
namespace LoanBook.Collections.Messaging.Commands
{
    public class StartDebtCollections : ICommand
    {
        public string Topic
        {
            get { return TopicNames.CollectionsCommands; }
        }
    }
}
