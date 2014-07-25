using LoanBook.Messaging;

namespace LoanBook.Financial.Messaging.Events
{
    public sealed class CashPaymentRegistered : IEvent 
    {
        public string Topic
        {
            get { return TopicNames.FinancialsEvents; }
        }
    }
}