using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ClassLibrary1
{
    [TestFixture]
    class LoanAgreementTests
    {
        [Test]
        public void CalculateInterestAfterOneDay()
        {
            DateTimeProvider.DateTimeFunction = () => new DateTime(2000, 01, 02);
            
            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 36.5m,
                StartDate = new DateTime(2000, 01, 01)
            };

            var balance = loanAgreement.GetBalance();

            Assert.IsTrue(balance == 110m);
        }

        [Test]
        public void CalculateCompoundInterestAfter()
        {
            DateTimeProvider.DateTimeFunction = () => new DateTime(2000, 01, 03);

            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 36.5m,
                StartDate = new DateTime(2000, 01, 01)
            };

            var balance = loanAgreement.GetBalance();

            Assert.IsTrue(balance == 121m);
        }

        [Test]
        public void CalculateCompoundInterestWithPaymentReceived()
        {
            DateTimeProvider.DateTimeFunction = () => new DateTime(2000, 01, 03);

            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 36.5m,
                StartDate = new DateTime(2000, 01, 01)
            };

            loanAgreement.ReceivePayment(new Payment { Amount = 10, Received = new DateTime(2000, 01, 02) });

            var balance = loanAgreement.GetBalance();

            Assert.IsTrue(balance == 110m);
        }

        [Test]
        public void BalanceIsClearedByFinalPayment()
        {
            DateTimeProvider.DateTimeFunction = () => new DateTime(2000, 01, 03);

            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 36.5m,
                StartDate = new DateTime(2000, 01, 03)
            };

            loanAgreement.ReceivePayment(new Payment { Amount = 121m, Received = new DateTime(2000, 01, 03) });

            var balance = loanAgreement.GetBalance();

            Assert.IsTrue(balance == 121m);
        }

        [Test]
        public void InstallmentPlans()
        {
            var loanAgreement = new LoanAgreement
            {
                Amount = 100,
                AnnualPercentageRate = 2500m,
                StartDate = new DateTime(2000, 01, 01),
                DueDate = new DateTime(2000, 01, 29),
                InstallmentPlans = new List<InstallmentPlan>()
            };

            var projectedBalance = loanAgreement.GetProjectedBalance();

            var installmentPlan = InstallmentPlan.Create(projectedBalance, 1, loanAgreement.DueDate);

            loanAgreement.SetInstallmentPlan(installmentPlan);

            var installment = loanAgreement.GetNextInstallment();
        }
    }
}
