using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using LoanBook.Origination.Core;
using LoanBook.Origination.Messaging.Commands;

namespace LoanBook.Origination.Endpoint.CommandHandlers
{
    public sealed class CreditCheckApplicationHandler : IHandleCommand<CreditCheckApplication>
    {
        private readonly OriginationContext _originationContext;
        private readonly IBureauService _bureauService;

        public CreditCheckApplicationHandler(OriginationContext originationContext, IBureauService bureauService)
        {
            _originationContext = originationContext;
            _bureauService = bureauService;
        }

        public void Handle(CreditCheckApplication creditCheckApplication)
        {
            var application = _originationContext.Applications.FirstOrDefault(a => a.Id == creditCheckApplication.ApplicationId);

            var response = _bureauService.Check(new BureauRequest());

        }
    }
}
