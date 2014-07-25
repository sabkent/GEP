using LoanBook.Financial.Core;
using LoanBook.Financial.Core.Dto;
using LoanBook.Financial.Messaging.Commands;
using LoanBook.Financial.Messaging.Events;
using LoanBook.Messaging;

namespace LoanBook.Financial.Endpoint.CommandHandlers
{
    public sealed class RegisterCashPaymentHandler : IHandleCommand<RegisterCashPayment>
    {
        private readonly IPublishEvents _eventPublisher;
        private readonly IFinancialServicesProvider _financialServicesProvider;

        public RegisterCashPaymentHandler(IPublishEvents eventPublisher, IFinancialServicesProvider financialServicesProvider)
        {
            _eventPublisher = eventPublisher;
            _financialServicesProvider = financialServicesProvider;
        }

        public void Handle(RegisterCashPayment command)
        {
            var directLoanPaymentRequest = new DirectLoanPaymentRequest();
            _financialServicesProvider.DirectLoanPayment(directLoanPaymentRequest);
            _eventPublisher.Publish(new CashPaymentRegistered());
        }
    }
}