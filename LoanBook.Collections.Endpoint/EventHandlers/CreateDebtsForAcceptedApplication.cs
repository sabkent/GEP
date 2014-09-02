using System;
using LoanBook.Collections.Core.Entities;
using LoanBook.Messaging;
using LoanBook.Origination.Messaging.Events;

namespace LoanBook.Collections.Endpoint.EventHandlers
{
    public sealed class CreateDebtsForAcceptedApplication : ISubscribeToEvent<ApplicationAccepted>
    {
        private readonly CollectionsContext _collectionsContext;

        public CreateDebtsForAcceptedApplication(CollectionsContext collectionsContext)
        {
            _collectionsContext = collectionsContext;
        }

        public void Notify(ApplicationAccepted applicationAccepted)
        {
            var debt = new Debt {Id = Guid.NewGuid(), DebtorId = applicationAccepted.ApplicantId, Amount = applicationAccepted.Amount};

            _collectionsContext.Debts.Add(debt);

            _collectionsContext.SaveChanges();
        }
    }
}
