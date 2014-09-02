using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Configuration
{
    public interface IDataProtector
    {
        byte[] Protect(byte[] data, string entropy = Constants.EmptyString);
        byte[] Unprotect(byte[] data, string entropy = Constants.EmptyString);
    }
}
