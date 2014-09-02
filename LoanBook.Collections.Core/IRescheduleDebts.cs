using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Collections.Core.Entities;

namespace LoanBook.Collections.Core
{
    public interface IRescheduleDebts
    {
        Debt Reshedule(Debt outstandingDebt);
    }

    public class DefaultDebtRescheduler : IRescheduleDebts
    {
        public Debt Reshedule(Debt outstandingDebt)
        {
            return new Debt
            {
                Id = Guid.NewGuid(),
                Amount = outstandingDebt.Amount,
                DebtorId = outstandingDebt.DebtorId,
                Due = outstandingDebt.Due.AddDays(1)
            };
        }
    }
}
