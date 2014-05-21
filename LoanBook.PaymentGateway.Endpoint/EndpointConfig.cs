
using System.Collections.Generic;
using LoanBook.Endpoint;

namespace LoanBook.PaymentGateway.Endpoint
{
    using System;

    using NServiceBus;
	
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantToRunWhenBusStartsAndStops
	{
        public void Start()
        {
            Console.Clear();
            Console.Title = "Payment gateway";
        }

        public void Stop()
        {

        }
    }
}
