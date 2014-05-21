
using System;

namespace LoanBook.Fraud.Endpoint
{
    using NServiceBus;

	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            Console.Clear();
            Console.Title = "Fraud";
        }

        public void Stop()
        {

        }
    }
}
