using LoanBook.Collections.Core;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Messaging.Events;
using System.Linq;

namespace LoanBook.Collections.Endpoint.EventHandlers
{
    public sealed class RescheduleDebtAfterFailedPaymentHandler : ISubscribeToEvent<PaymentFailed>
    {
        private readonly CollectionsContext _collectionsContext;
        private readonly IRescheduleDebts _debtRescheduler;

        public RescheduleDebtAfterFailedPaymentHandler(CollectionsContext collectionsContext, IRescheduleDebts debtRescheduler)
        {
            _collectionsContext = collectionsContext;
            _debtRescheduler = debtRescheduler;
        }

        public void Notify(PaymentFailed paymentFailed)
        {
            var outstandingDebt = _collectionsContext.Debts.FirstOrDefault(debt => debt.Id == paymentFailed.CorrelationId);
            var rescheduledDebt = _debtRescheduler.Reshedule(outstandingDebt);

            _collectionsContext.Debts.Add(rescheduledDebt);
            _collectionsContext.SaveChanges();
        }
    }
}
