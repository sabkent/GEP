using System;
using Autofac;
using LoanBook.Endpoint;
using LoanBook.Infrastructure;

namespace LoanBook.CustomerRelationshipManagement.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<EndpointModule>();
            containerBuilder.RegisterModule<InfrastructureModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            Console.ReadKey();
        }
    }
}