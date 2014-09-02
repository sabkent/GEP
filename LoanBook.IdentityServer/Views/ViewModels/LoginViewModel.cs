using LoanBook.IdentityServer.Configuration;
using System.Collections.Generic;

namespace LoanBook.IdentityServer.Views.ViewModels
{
    public class LoginViewModel : ErrorViewModel
    {
        public string LoginUrl { get; set; }
        public string UserName { get; set; }
        public IEnumerable<LoginPageLink> ExternalProviders { get; set; }
        public IEnumerable<LoginPageLink> AdditionsLinks { get; set; }
    }
}
