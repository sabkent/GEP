using System;
using System.Collections.Generic;

namespace ClassLibrary1.Collections
{
    class DebtQueries : IQueryDebts
    {
        public IEnumerable<Debt> GetDebtsDue(int attempt, DateTime dueDate)
        {
            return new List<Debt>
            {
                new Debt()
            };
        }
    }
}
