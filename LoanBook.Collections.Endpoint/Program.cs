using System.Data.Entity;
using Autofac;
using LoanBook.Collections.Core.Entities;
using LoanBook.Collections.Messaging.Commands;
using LoanBook.Infrastructure;
using System;
using LoanBook.Messaging;

namespace LoanBook.Collections.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CollectionsContext>());

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<MessagingModule>();
            containerBuilder.RegisterModule<CollectionsModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            var content = container.Resolve<CollectionsContext>();
            content.Debts.Add(new Debt { Id = Guid.Parse("7ff4e4f4-02b6-4161-9b12-b7a74170a9f7"), DebtorId = Guid.Parse("de1a4e89-339a-42b1-bff7-ef6130bfe80e"), Amount = 100, Due = DateTime.Now.Date});
            //content.Debts.Add(new Debt { Id = Guid.Parse("c574651f-6a72-41b8-a1d5-006d00c81d39"), DebtorId = Guid.Parse("1b9d1848-1ab3-4413-820a-5816b82b3b19"), Amount = 150 });
            content.SaveChanges();

            var handler = container.Resolve<IHandleCommand<StartDebtCollections>>();

            handler.Handle(new StartDebtCollections());
            Console.ReadKey();
        }
    }
}
