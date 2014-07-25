using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1.Collections
{
    public interface IDebtRepository
    {
        void Add(Debt nextAttempt);
    }
}
