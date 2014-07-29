using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ClassLibrary1.Collections.Entity;

namespace ClassLibrary1.Collections
{
    class CollectDebtsHandler
    {
        private readonly CollectionsContext _collectionsContext;
        private readonly ICollectDebtRetryStrategy _collectDebtRetryStrategy;
        private readonly IPaymentGatewayProvider _paymentGatewayProvider;

        public CollectDebtsHandler(CollectionsContext collectionsContext, ICollectDebtRetryStrategy collectDebtRetryStrategy, IPaymentGatewayProvider paymentGatewayProvider)
        {
            _collectionsContext = collectionsContext;
            _collectDebtRetryStrategy = collectDebtRetryStrategy;
            _paymentGatewayProvider = paymentGatewayProvider;
        }

        public async void Handle(CollectDebts collectDebts)
        {
            var debts = _collectionsContext.Debts.Where(debt => debt.Attempt == collectDebts.Attempt).ToList();

            debts.ForEach(debt =>
            {
                var collectionAttempt = new CollectionAttempt {DebtId = debt.Id, Attempt = debt.Attempt};
                _collectionsContext.CollectionAttempts.Add(collectionAttempt);
                try
                {
                    _collectionsContext.SaveChanges();
                    var takePaymentResult = _paymentGatewayProvider.TakePayment(MapToTakePaymentRequest(debt)).Result;
                    if (takePaymentResult.IsSuccess)
                    {
                        collectionAttempt.Reference = takePaymentResult.Reference;
                        _collectionsContext.SaveChanges();
                        //publish event
                    }
                    else
                    {
                        var nextAttempt = _collectDebtRetryStrategy.GetNextAttemptFor(debt);

                        _collectionsContext.Debts.Add(nextAttempt);
                        _collectionsContext.SaveChanges();
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    
                }
            });
        }

        private TakePaymentRequest MapToTakePaymentRequest(Debt debt)
        {
            return new TakePaymentRequest
            {
                Amount = debt.Amount
            };
        }
    }
}