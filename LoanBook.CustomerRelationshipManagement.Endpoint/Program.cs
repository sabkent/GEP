using System;
using Autofac;
using LoanBook.Infrastructure;
using LoanBook.Messaging;

namespace LoanBook.CustomerRelationshipManagement.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<CustomerRelationshipManagementModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            Console.ReadKey();
        }
    }
}