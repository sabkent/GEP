using System;
using LoanBook.Financial.Messaging.Events;
using LoanBook.Messaging;

namespace LoanBook.CustomerRelationshipManagement.Endpoint.EventHandlers
{
    public sealed class RecordActivityForCashPaymentRegistered : ISubscribeToEvent<CashPaymentRegistered>
    {
        public void Notify(CashPaymentRegistered cashPaymentRegistered)
        {
            bool pass = true;
        }
    }
}