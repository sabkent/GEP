using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Collections.Core.Data;
using LoanBook.Collections.Core.Entities;
using LoanBook.Collections.Core.Query;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Messaging.Commands;

using LoanBook.Collections.Messaging.Commands;

namespace LoanBook.Collections.Endpoint.CommandHandlers
{
    internal sealed class StartDebtCollectionsHandler : IHandleCommand<StartDebtCollections>
    {
        private readonly CollectionsContext _collectionsContext;
        private readonly IDispatchCommands _commandDispatcher;
        private readonly IQueryPayments _queryPayments;
        private readonly IPaymentRepository _paymentRepository;

        public StartDebtCollectionsHandler(CollectionsContext collectionsContext, IDispatchCommands commandDispatcher, IQueryPayments queryPayments, IPaymentRepository paymentRepository)
        {
            _collectionsContext = collectionsContext;
            _commandDispatcher = commandDispatcher;
            _queryPayments = queryPayments;
            _paymentRepository = paymentRepository;
        }

        public void Handle(StartDebtCollections message)
        {
            var debts = _collectionsContext.Debts.ToList();

            debts.ForEach(debt =>
            {
                var collection = new Collection {DebtId = debt.Id};
                _collectionsContext.Collections.Add(collection);

                _collectionsContext.SaveChanges();

                var takePayment = new TakePayment {AccountId = debt.DebtorId, Amount = debt.Amount};
                _commandDispatcher.Send(takePayment);
            });

            //var collection = new Collection{DebtId = de}


            //var duePayments = _queryPayments.GetPaymentDueFor(DateTime.Now.Date).ToList();

            //foreach (var duePayment in duePayments)
            //{
            //    //load payment and ask it if it can be collected
            //    _commandDispatcher.Send(new TakePayment());
            //}

        }
    }
}
