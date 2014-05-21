
using System;

namespace LoanBook.CustomerRelationshipManagement.Endpoint
{
    using Autofac;

    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization, IWantToRunWhenBusStartsAndStops
    {
        public void Init()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CustomerRelationshipManagementModule());
            Configure.With().PurgeOnStartup(true).AutofacBuilder(containerBuilder.Build());
        }

        public void Start()
        {
            Console.Clear();
            Console.Title = "Customer Relationship Management";
        }

        public void Stop()
        {
        }
    }
}
