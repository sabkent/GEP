using System;
using LoanBook.CustomerRelationshipManagement.Messaging.Commands;
using LoanBook.Messaging;

namespace LoanBook.CustomerRelationshipManagement.Endpoint.CommandHandlers
{
    public sealed class RecordActivityHandler : IHandleCommand<RecordActivity>
    {
        public void Handle(RecordActivity command)
        {
            bool pass = true;
        }
    }
}
