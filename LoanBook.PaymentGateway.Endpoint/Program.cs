using System;
using System.Data.Entity;
using Autofac;
using LoanBook.Infrastructure;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Core;

namespace LoanBook.PaymentGateway.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<MessagingModule>();
            containerBuilder.RegisterModule<PaymentGatewayModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            Database.SetInitializer(new DropCreateDatabaseAlways<PaymentGatewayContext>());
            var context = container.Resolve<PaymentGatewayContext>();

            var cardHolder = new CardHolder
            {
                Id = Guid.Parse("de1a4e89-339a-42b1-bff7-ef6130bfe80e"),
                FirstName = "seb",
                LastName = "kent",
                BillingAddress = new Address
                {
                    CountryIsoCode = "GB",
                    City = "London",
                    LineOne = "80a Hackford road",
                    PostalCode = "SW9 0RG",
                    RegionState = "Lambeth"
                },
                PaymentCard = new Card
                {
                    Number = "4417119669820331",
                    Cvv2 = "874",
                    Type = "visa",
                    Expires = DateTime.Now.AddMonths(6)
                }
            };
            context.CardHolders.Add(cardHolder);
            context.SaveChanges();

            Console.ReadKey();
        }
    }
}
