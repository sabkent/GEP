using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using LoanBook.Origination.Messaging.Commands;
using LoanBook.Origination.Messaging.Events;

namespace LoanBook.Origination.Endpoint.EventHandlers
{
    public class ApplicationSubmittedHandler : ISubscribeToEvent<ApplicationSubmitted>
    {
        private readonly IDispatchCommands _commandDispatcher;

        public ApplicationSubmittedHandler(IDispatchCommands commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Notify(ApplicationSubmitted @event)
        {
            var creditCheckApplication = new CreditCheckApplication();
            _commandDispatcher.Send(creditCheckApplication);
        }
    }
}
