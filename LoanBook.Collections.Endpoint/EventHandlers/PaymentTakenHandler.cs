using System.Linq;
using System.Net;
using LoanBook.Messaging;
using System;
using LoanBook.PaymentGateway.Messaging.Events;

namespace LoanBook.Collections.Endpoint.EventHandlers
{
    public sealed class PaymentTakenHandler : ISubscribeToEvent<PaymentTaken>
    {
        private readonly CollectionsContext _collectionsContext;

        public PaymentTakenHandler(CollectionsContext collectionsContext)
        {
            _collectionsContext = collectionsContext;
        }

        public void Notify(PaymentTaken paymentTaken)
        {
            var collection = _collectionsContext.Collections.FirstOrDefault(c => c.DebtId == paymentTaken.CorrelationId);

            if (collection == null)
                throw new Exception("No corresponding collection started for DebtId: " + paymentTaken.CorrelationId);

            collection.Succedded = paymentTaken.Succeeded;
            collection.Reference = paymentTaken.ProviderReference;

            _collectionsContext.SaveChanges();
        }
    }
}
