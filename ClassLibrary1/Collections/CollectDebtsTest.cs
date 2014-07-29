using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ClassLibrary1.Collections.Entity;
using Moq;
using NUnit.Framework;
using System;

namespace ClassLibrary1.Collections
{
    [TestFixture]
    public class CollectDebtsTest
    {
        [SetUp]
        public void Setup()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CollectionsContext>());
        }

        [Test]
        public void MakeFirstAttempt()
        {
            CollectionsContext context = new CollectionsContext();
            context.Debts.Add(new Debt {Amount = 100, Due = new DateTime(2000, 01, 01), Attempt = 1, Id = Guid.NewGuid()});
            context.Debts.Add(new Debt { Amount = 100, Due = new DateTime(2000, 01, 01), Attempt = 1, Id = Guid.NewGuid()});
            context.SaveChanges();
            
            var collectDebts = new CollectDebts
            {
                Attempt = 1,
                DueDate = new DateTime(2000, 01, 01)
            };

            var paymentGatewayProvider = new Mock<IPaymentGatewayProvider>();
            paymentGatewayProvider.Setup(gateway=>gateway.TakePayment(It.IsAny<TakePaymentRequest>())).Returns(Task.FromResult(new TakePaymentResponse{IsSuccess = true, Reference = "pay ref"}));
            
            var collectDebtsHandler = new CollectDebtsHandler(context, new ContinousPaymentAuthorityDebtCollectionRetry(), paymentGatewayProvider.Object);
            
            collectDebtsHandler.Handle(collectDebts);
            
        }

        [Test]
        public void UniqueKey()
        {
            var context = new CollectionsContext();
            var debtId = Guid.NewGuid();
            var collectionAttemptOne = new CollectionAttempt {DebtId = debtId, Attempt = 1};
            var collectionAttemptTwo = new CollectionAttempt { DebtId = debtId, Attempt = 1 };

            context.CollectionAttempts.AddRange(new[] {collectionAttemptOne, collectionAttemptTwo});
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sex = ex.GetBaseException() as SqlException;
                if (sex.Number == 2671) //violation primary key
                {
                    
                }
            }

        }
    }
}
