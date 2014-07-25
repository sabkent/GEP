using System;
using LoanBook.CustomerRelationshipManagement.Messaging.Commands;
using LoanBook.Financial.Messaging.Events;
using LoanBook.Messaging;

namespace LoanBook.CustomerRelationshipManagement.Endpoint.EventHandlers
{
    public sealed class RecordActivityForCashPaymentRegistered : ISubscribeToEvent<CashPaymentRegistered>
    {
        private readonly IDispatchCommands _commandDispatcher;

        public RecordActivityForCashPaymentRegistered(IDispatchCommands commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Notify(CashPaymentRegistered cashPaymentRegistered)
        {
            var recordActivity = new RecordActivity();
            _commandDispatcher.Send(recordActivity);
        }
    }
}