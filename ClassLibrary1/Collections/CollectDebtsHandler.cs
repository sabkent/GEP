using System.Linq;

namespace ClassLibrary1.Collections
{
    class CollectDebtsHandler
    {
        private readonly IPaymentGatewayProvider _paymentGatewayProvider;

        public CollectDebtsHandler(IPaymentGatewayProvider paymentGatewayProvider)
        {
            _paymentGatewayProvider = paymentGatewayProvider;
        }

        public async void Handle(CollectDebts collectDebts)
        {
            var context = new CollectionsContext();

            var debts = context.Debts.Where(debt => debt.Attempt == collectDebts.Attempt).ToList();

            debts.AsParallel().ForAll(async debt =>
            {

                var takePaymentResult = await _paymentGatewayProvider.TakePayment(MapToTakePaymentRequest(debt));
                if (takePaymentResult.IsSuccess)
                {
                    //publish event
                }
                else
                {
                    var nextAttempt = new Debt(); //some strategy about when next attempt should occur

                }

                //unitOfWork.Commit();
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