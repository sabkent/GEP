
using System;

namespace LoanBook.Financial.Endpoint
{
    using Autofac;

    using NServiceBus;
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization, IWantToRunWhenBusStartsAndStops
    {
        public void Init()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new FinancialModule());
            Configure.With().PurgeOnStartup(true).AutofacBuilder(containerBuilder.Build());
        }

        public void Start()
        {
            Console.Clear();
            Console.Title = "Financial";
        }

        public void Stop()
        {

        }
    }
}
