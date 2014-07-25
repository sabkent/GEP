using System;
using System.Collections.Generic;

namespace ClassLibrary1.Collections
{
    public interface IQueryDebts
    {
        IEnumerable<Debt> GetDebtsDue(int attempt, DateTime dueDate);
    }
}
