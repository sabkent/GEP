using System.Linq;
using LoanBook.Messaging;
using System;
using LoanBook.PaymentGateway.Core;
using LoanBook.PaymentGateway.Messaging.Commands;
using LoanBook.PaymentGateway.Messaging.Events;

namespace LoanBook.PaymentGateway.Endpoint.CommandHandlers
{
    public class TakePaymentCommandHandler : IHandleCommand<TakePayment>
    {
        private readonly PaymentGatewayContext _paymentGatewayContext;
        private readonly IPublishEvents _eventsPublisher;
        private readonly IPaymentGateway _paymentGateway;

        public TakePaymentCommandHandler(PaymentGatewayContext paymentGatewayContext, IPublishEvents eventsPublisher, IPaymentGateway paymentGateway)
        {
            _paymentGatewayContext = paymentGatewayContext;
            _eventsPublisher = eventsPublisher;
            _paymentGateway = paymentGateway;
        }

        public void Handle(TakePayment takePayment)
        {
            var cardHolder = _paymentGatewayContext.CardHolders.FirstOrDefault(x => x.Id == takePayment.CardHolderId);

            var directPaymentRequest = new DirectPaymentRequest()
            {
                Amount = takePayment.Amount,
                CardHolder = cardHolder
            };

            var directPaymentResponse = _paymentGateway.DirectPayment(directPaymentRequest);

            _eventsPublisher.Publish(new PaymentTaken{CorrelationId = takePayment.CorrelationId, ProviderReference = directPaymentResponse.Reference, Succeeded = true});
        }
    }
}