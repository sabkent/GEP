using LoanBook.Financial.Messaging.Commands;
using LoanBook.Financial.Messaging.Events;
using LoanBook.Messaging;

namespace LoanBook.Financial.Endpoint.CommandHandlers
{
    public sealed class RegisterCashPaymentHandler : IHandleCommand<RegisterCashPayment>
    {
        private readonly IPublishEvents _eventPublisher;
        
        public RegisterCashPaymentHandler(IPublishEvents eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(RegisterCashPayment command)
        {
            _eventPublisher.Publish(new CashPaymentRegistered());
        }
    }
}