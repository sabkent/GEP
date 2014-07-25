using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using NUnit.Framework;

namespace ClassLibrary1
{
    public partial class LoanAgreement
    {
        private List<Payment> _payments { get; set; }
        private List<Installment> _defaults { get; set; }

        public LoanAgreement()
        {
            _payments = new List<Payment>();
        }

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal AnnualPercentageRate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        
        public List<InstallmentPlan> InstallmentPlans { get; set; }
        public decimal GetBalance()
        {
            decimal balance = Amount;

            var days = DateTimeProvider.Now.Subtract(StartDate).Days;

            for (int i = 0; i < days; i++)
            {
                var received = _payments.Where(payment => payment.Received.Date.Equals(StartDate.Date.AddDays(i))).Sum(payment => payment.Amount);
                balance -= received;
                var interest = balance * GetDailyPercentageRate();
                balance += interest;
            }

            return balance;
        }

        public decimal GetProjectedBalance()
        {
            var days = DueDate.Subtract(StartDate.Date).Days;
            var balance = Amount;
            for (int i = 0; i < days; i++)
            {
                var interest = balance * GetDailyPercentageRate();
                balance += interest;
            }
            return Decimal.Round(balance, 2, MidpointRounding.AwayFromZero);
        }

        public void SetInstallmentPlan(InstallmentPlan installmentPlan)
        {
            InstallmentPlans.Add(installmentPlan);
        }

        public void ReceivePayment(Payment payment)
        {
            _payments.Add(payment);
        }

        public void DefaultInstallment(Installment installment)
        {
            _defaults.Add(installment);
        }

        public Installment GetNextInstallment()
        {
            var installmentPlan = InstallmentPlans.OrderByDescending(plan => plan.Created).FirstOrDefault();
            return installmentPlan.GetNextInstallment();
        }

        //http://windowssecrets.com/forums/showthread.php/25918-Converting-annual-interest-rates-to-a-daily-rate-(2000-SR1)
        private decimal GetDailyPercentageRate()
        {
            var annualPercentage = AnnualPercentageRate/100;
            var daily = 1m/365m;

            return (decimal) Math.Pow((double) annualPercentage + 1, (double) daily) - 1;
        }
       
    }

    partial class LoanAgreement
    {
        public class LoanAgreementConfiguration : EntityTypeConfiguration<LoanAgreement>
        {
            public LoanAgreementConfiguration()
            {
                HasKey(x => x.Id);
                Property(x => x.Amount);
                HasMany(x => x._payments);
            }
        }
    }
}
