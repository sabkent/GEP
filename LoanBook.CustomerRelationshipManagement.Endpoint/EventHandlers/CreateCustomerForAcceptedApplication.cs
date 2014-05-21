using System;

namespace LoanBook.CustomerRelationshipManagement.Endpoint.EventHandlers
{
    using LoanBook.Origination.Messaging.Events;

    using NServiceBus;

    internal sealed class CreateCustomerForAcceptedApplication : IHandleMessages<ApplicationAccepted>
    {
        public void Handle(ApplicationAccepted applicationAccepted)
        {
            //what if it was a duplicate, then a new record should not be created
            Console.WriteLine("create customer for application accepted");
        }
    }
}
