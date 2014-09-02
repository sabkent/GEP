using System.Data.Entity;
using Autofac;
using LoanBook.Infrastructure;
using LoanBook.Messaging;
using System;
using LoanBook.Origination.Messaging.Events;
using LoanBook.PaymentGateway.Messaging.Events;

namespace LoanBook.Origination.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<OriginationContext>());

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<OriginationModule>();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<MessagingModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            var input = String.Empty;
            do
            {
                input = Console.ReadLine().ToLower().Trim();
                var command = input;
                var parts = input.Split(new[] {' '});
                if (parts.Length > 1)
                    command = parts[0];

                switch (command)
                {
                    case "cardsaved":
                        container.Resolve<IPublishEvents>().Publish(new CardSavedForApplication());
                        break;
                    case "submitaccepted":
                        container.Resolve<IPublishEvents>().Publish(new ApplicationSubmissionAccepted());
                        break;
                }

            } while (input.Equals("quit") == false);
        }
    }
}
