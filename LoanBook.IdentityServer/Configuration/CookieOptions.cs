using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Configuration
{
    public sealed class CookieOptions
    {
        public string Prefix { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }
        public bool IsPersistent { get; set; }
        public bool SlidingExpiration { get; set; }
    }
}
