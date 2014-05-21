using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Financial.Core
{
    public interface IAccountRepository
    {
        void Add(Account account);
    }
}
