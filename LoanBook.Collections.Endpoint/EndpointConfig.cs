

using System;
using System.Data.SqlClient;
using Autofac;
using LoanBook.Collections.Core;
using LoanBook.Collections.Core.Data;
using LoanBook.Collections.Messaging.Commands;

namespace LoanBook.Collections.Endpoint
{
    using NServiceBus;
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization, IWantToRunWhenBusStartsAndStops
    {
	    public IBus Bus { get; set; }

        public void Init()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new CollectionsModule());

            var container = containerBuilder.Build();

            Configure.With().AutofacBuilder(container);
        }

        public void Start()
        {
            Console.Clear();
            Console.Title = "Collections";
        }

        public void Stop()
        {
            
        }

        
    }
}
