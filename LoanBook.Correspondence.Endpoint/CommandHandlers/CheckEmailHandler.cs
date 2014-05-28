namespace LoanBook.Correspondence.Endpoint.CommandHandlers
{
    using System;
    using System.Collections.Generic;

    using LoanBook.Correspondence.Core;
    using LoanBook.Correspondence.Infrastructure.MailServers;
    using LoanBook.Correspondence.Messaging.Commands;
    using LoanBook.Correspondence.Messaging.Events;

    using NServiceBus;

    class CheckEmailHandler : IHandleMessages<CheckEmail>
    {
        //private readonly IBus bus;

        //public CheckEmailHandler(IBus bus)
        //{
        //    this.bus = bus;
        //}

        public void Handle(CheckEmail checkEmail)
        {
            var providers = GetMailServerProvider();

            //foreach (var provider in providers)
            //{
            //    var messages = provider.GetMessages();

            //    foreach (var message in messages)
            //    {
            //        var emailReceived = new EmailReceived(); //automap
            //        this.bus.Publish(emailReceived);
            //    }
            //}
        }

        private IEnumerable<IMailServerProvider> GetMailServerProvider()
        {
            return new List<IMailServerProvider> { new FakeMailServiceProvider() };
        }

        class FakeMailServiceProvider : IMailServerProvider
        {
            public IEnumerable<EmailMessage> GetMessages()
            {
                return new List<EmailMessage> { new EmailMessage { } };
            }
        }

    }
}
