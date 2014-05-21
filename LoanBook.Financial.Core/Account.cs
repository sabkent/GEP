using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Financial.Core
{
    public class Account
    {
        public Guid Id { get; set; }
        private List<Entry> _entries;

        public Account()
        {
            _entries = new List<Entry>();
        }

        public void AddPrincipal(decimal principal)
        {
            _entries.Add(new Entry{Amount = principal});
        }

        public void AddInterest(decimal interest)
        {
            _entries.Add(new Entry { Amount = interest });
        }

        public decimal GetBalance()
        {
            return _entries.Sum(entry => entry.Amount);
        }
    }
}
