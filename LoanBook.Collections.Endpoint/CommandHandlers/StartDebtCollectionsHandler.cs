using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Collections.Core.Data;
using LoanBook.Collections.Core.Query;
using LoanBook.PaymentGateway.Messaging.Commands;
using NServiceBus;
using LoanBook.Collections.Messaging.Commands;

namespace LoanBook.Collections.Endpoint.CommandHandlers
{
    internal sealed class StartDebtCollectionsHandler : IHandleMessages<StartDebtCollections>
    {
        private readonly IBus _bus;
        private readonly IQueryPayments _queryPayments;
        private readonly IPaymentRepository _paymentRepository;

        public StartDebtCollectionsHandler(IBus bus, IQueryPayments queryPayments, IPaymentRepository paymentRepository)
        {
            _bus = bus;
            _queryPayments = queryPayments;
            _paymentRepository = paymentRepository;
        }

        public void Handle(StartDebtCollections message)
        {
            Console.WriteLine("starting debt collection...");

            var duePayments = _queryPayments.GetPaymentDueFor(DateTime.Now.Date).ToList();

            Console.WriteLine("found {0} payments due today", duePayments.Count);

            foreach (var duePayment in duePayments)
            {
                //load payment and ask it if it can be collected
                _bus.Send(new TakePayment());
            }

        }
    }
}
