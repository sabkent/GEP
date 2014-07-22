using LoanBook.Messaging;

namespace LoanBook.Financial.Messaging.Commands
{
    public sealed class RegisterCashPayment : ICommand
    {
        public string Topic
        {
            get { return TopicNames.Financials; }
        }
    }
}