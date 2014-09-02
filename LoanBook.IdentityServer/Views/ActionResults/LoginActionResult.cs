using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Views.ViewModels;
using System.Collections.Generic;

namespace LoanBook.IdentityServer.Views.ActionResults
{
    internal class LoginActionResult : HtmlStreamActionResult
    {
        public LoginActionResult(IViewService viewService, IDictionary<string, object> environment, LoginViewModel loginViewModel)
            : base(async () => await viewService.Login(environment, loginViewModel))
        {
            
        }
    }
}
