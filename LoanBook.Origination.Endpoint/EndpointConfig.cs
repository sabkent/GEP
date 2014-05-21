
using System.Collections.Generic;
using LoanBook.Endpoint;

namespace LoanBook.Origination.Endpoint
{
    using System;

    using Autofac;

    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization, IWantToRunWhenBusStartsAndStops
    {
        public void Init()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new OriginationModule());
            Configure.With().PurgeOnStartup(true).AutofacBuilder(containerBuilder.Build());
        }

        public void Start()
        {
            Console.Clear();
            Console.Title = "Origination";
        }

        public void Stop()
        {
        }
    }
}
