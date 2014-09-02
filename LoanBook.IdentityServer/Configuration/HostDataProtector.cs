using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;

namespace LoanBook.IdentityServer.Configuration
{
    internal sealed class HostDataProtector : IDataProtector
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public HostDataProtector(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public byte[] Protect(byte[] data, string entropy = Constants.EmptyString)
        {
            var protector = _dataProtectionProvider.Create(entropy);
            return protector.Protect(data);
        }

        public byte[] Unprotect(byte[] data, string entropy = Constants.EmptyString)
        {
            var protector = _dataProtectionProvider.Create(entropy);
            return protector.Unprotect(data);
        }
    }
}
