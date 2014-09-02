using System;
using System.Collections.Generic;
using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Views.ViewModels;

namespace LoanBook.IdentityServer.Views.ActionResults
{
    internal class ErrorActionResult : HtmlStreamActionResult
    {
        public ErrorActionResult(IViewService viewService, IDictionary<string, object> environment, ErrorViewModel errorViewModel)
            : base(async () => await viewService.Error(environment, errorViewModel))
        {
            if(viewService == null) throw new ArgumentNullException("viewService");
            if(environment == null) throw new ArgumentNullException("environment");
            if(errorViewModel == null) throw new ArgumentNullException("errorViewModel");
        }
    }
}