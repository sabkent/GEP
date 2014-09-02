using System.Collections.Generic;

namespace LoanBook.IdentityServer.Configuration
{
    public class AuthenticationOptions
    {
        public IEnumerable<LoginPageLink> LoginPageLinks { get; set; }
    }
}