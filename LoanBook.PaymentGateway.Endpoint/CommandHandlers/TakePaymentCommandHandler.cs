using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;

namespace LoanBook.PaymentGateway.Endpoint.CommandHandlers
{
    using LoanBook.PaymentGateway.Messaging.Commands;
    using LoanBook.PaymentGateway.Messaging.Events;
    
    class TakePaymentCommandHandler : IHandleCommand<TakePayment>
    {
        private readonly IPublishEvents _eventPublisher;

        public TakePaymentCommandHandler(IPublishEvents eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(TakePayment message)
        {
            Console.WriteLine("Take payment handler invoked");
            _eventPublisher.Publish(new PaymentTaken());
        }
    }
}
