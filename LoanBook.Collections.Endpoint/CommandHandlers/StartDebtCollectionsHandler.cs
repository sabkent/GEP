using LoanBook.Collections.Core.Data;
using LoanBook.Collections.Core.Entities;
using LoanBook.Collections.Core.Query;
using LoanBook.Collections.Messaging.Commands;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Messaging.Commands;
using System.Linq;

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

            debts.ForEach(ProcessDebt);
        }

        private void ProcessDebt(Debt debt)
        {
            var collection = new Collection { DebtId = debt.Id };
            _collectionsContext.Collections.Add(collection);

            _collectionsContext.SaveChanges();

            var takePayment = new TakePayment{CardHolderId = debt.DebtorId, Amount = debt.Amount, CorrelationId = debt.Id};
            _commandDispatcher.Send(takePayment);
        }
    }
}
