using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class InstallmentPlan
    {
        public Guid Id { get; set; }
        public List<Installment> Installments { get; set; }
        public DateTime Created { get; set; }

        public static InstallmentPlan Create(decimal amount, int installmentCount, DateTime firstInstallment)
        {
            decimal installmentPortion = amount/installmentCount; //TODO: round all to nearest 5 final installment can be less so 100 over 3 installs 35, 35, 30

            var installmentPlan = new InstallmentPlan();
            installmentPlan.Installments = new List<Installment>
            {
                new Installment(){Amount = installmentPortion, DueDate = firstInstallment}
            };

            return installmentPlan;
        }

        internal Installment GetNextInstallment()
        {
            return
                Installments.Where(installment => installment.DueDate.Date >= DateTimeProvider.Now.Date)
                    .OrderByDescending(installment => installment.DueDate).FirstOrDefault();
        }
    }

    interface ITimeSequence
    {
        IEnumerable<DateTime> GetDates(int count);
    }

    class DailyTimeFrame : ITimeSequence
    {
        private DateTime _startDate;

        public DailyTimeFrame(DateTime startDate)
        {
            _startDate = startDate;
        }

        public IEnumerable<DateTime> GetDates(int count)
        {
            List<DateTime> dates = new List<DateTime>{_startDate};
            dates.Add(_startDate.Add(TimeSpan.FromDays(1)));
            return dates;
        }
    }

}
