using LoanBook.LoanManagementSystem.Models;
using LoanBook.Messaging;
using LoanBook.Origination.Messaging.Commands;
using Microsoft.AspNet.SignalR;
using System;

namespace LoanBook.LoanManagementSystem.Hubs
{
    public class Application : Hub
    {
        private readonly IDispatchCommands _commandDispatcher;

        public Application(IDispatchCommands commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void SubmitApplication(ApplicationSubmission applicationSubmission)
        {
            var applicationId = Guid.NewGuid();

            Groups.Add(Context.ConnectionId, applicationId.ToString());

            var submitApplication = new SubmitApplication
            {
                ApplicationId = applicationId
            };

            _commandDispatcher.Send(submitApplication);
        }
    }
}