using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using LoanBook.Origination.Core;
using LoanBook.Origination.Messaging.Commands;
using LoanBook.Origination.Messaging.Events;

namespace LoanBook.Origination.Endpoint.CommandHandlers
{
    /// <summary>
    /// dedupes the application
    /// validates data required is supplied
    /// </summary>
    public class SubmitApplicationHandler : IHandleCommand<SubmitApplication>
    {
        private readonly OriginationContext _originationContext;
        private readonly IPublishEvents _eventsPublisher;

        public SubmitApplicationHandler(OriginationContext originationContext, IPublishEvents eventsPublisher)
        {
            _originationContext = originationContext;
            _eventsPublisher = eventsPublisher;
        }

        public void Handle(SubmitApplication command)
        {
            _originationContext.Applications.Add(new Application());
            _originationContext.SaveChanges();

            _eventsPublisher.Publish(new ApplicationAccepted());
        }
    }
}
