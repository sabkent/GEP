using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using System;
using NUnit.Framework.Constraints;

namespace ClassLibrary1.Collections
{
    [TestFixture]
    public class CollectDebtsTest
    {
        [Test]
        public void MakeFirstAttempt()
        {
            CollectionsContext context = new CollectionsContext();
            context.Debts.Add(new Debt {Amount = 100, Due = new DateTime(2000, 01, 01), Attempt = 1});
            context.SaveChanges();
            
            var collectDebts = new CollectDebts
            {
                Attempt = 1,
                DueDate = new DateTime(2000, 01, 01)
            };

            var paymentGatewayProvider = new Mock<IPaymentGatewayProvider>();
            paymentGatewayProvider.Setup(gateway=>gateway.TakePayment(It.IsAny<TakePaymentRequest>())).Returns(Task.FromResult(new TakePaymentResponse{IsSuccess = false}));
            
            var collectDebtsHandler = new CollectDebtsHandler(paymentGatewayProvider.Object);
            
            collectDebtsHandler.Handle(collectDebts);
            
        }
    }
}
