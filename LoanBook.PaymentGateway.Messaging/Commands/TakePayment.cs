using System;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Messaging.Commands
{
    public class TakePayment : ICommand
    {
        public string Topic
        {
            get { return TopicNames.PaymentGatewayCommands; }
        }

        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
    }
}