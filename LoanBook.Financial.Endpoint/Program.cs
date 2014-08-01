using System;
using Autofac;
using LoanBook.Infrastructure;
using LoanBook.Messaging;

namespace LoanBook.Financial.Endpoint
{
    class Program
    {
        private static IMessageDispatcher _messageDispatcher;

        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<FinancialModule>();
            containerBuilder.RegisterModule<InfrastructureModule>();

            var container = containerBuilder.Build();

            _messageDispatcher = container.Resolve<IMessageDispatcher>();
            _messageDispatcher.Listen();

            Console.ReadKey();
        }
    }
}