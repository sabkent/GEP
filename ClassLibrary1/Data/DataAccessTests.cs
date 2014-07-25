using NUnit.Framework;
using System;
using System.Data.Entity;

namespace ClassLibrary1.Data
{
    [TestFixture]
    class DataAccessTests
    {
        [Test]
        public void test()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MyDbContext>());

            MyDbContext context = new MyDbContext();

            var basic = new Basic();
            basic.DoIt();

            context.Basics.Add(basic);
            context.SaveChanges();
        }

        [Test]
        public void LoanAgreements()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MyDbContext>());

            MyDbContext context = new MyDbContext();

            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 2000,
                DueDate = new DateTime(2014, 08, 16),
                StartDate = new DateTime(2014, 08, 01)
            };
            loanAgreement.ReceivePayment(new Payment{Amount = 100, Received = DateTime.Now});

            context.LoanAgreements.Add(loanAgreement);
            

            context.SaveChanges();
        }
    }
}
