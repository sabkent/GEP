using System;
using LoanBook.Collections.Core.Data;
using LoanBook.Messaging;
using LoanBook.Origination.Messaging.Events;
using LoanBook.Collections.Core;

namespace LoanBook.Collections.Endpoint.EventHandlers
{
    internal sealed class CreatePaymentsForAcceptedApplication : ISubscribeToEvent<ApplicationAccepted>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentsForAcceptedApplication(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void Handle(ApplicationAccepted applicationAccepted)
        {
            var payment = new Payment {Id = Guid.NewGuid(), Amount = 100, DueDate = DateTime.Now.Date};
            _paymentRepository.Add(payment);
        }

        public void Notify(ApplicationAccepted @event)
        {
            throw new NotImplementedException();
        }
    }
}
