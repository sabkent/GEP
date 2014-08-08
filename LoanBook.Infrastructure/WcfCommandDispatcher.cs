using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.ServiceModel.Discovery.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LoanBook.Messaging;

namespace LoanBook.Infrastructure
{
    public class WcfCommandDispatcher : IDispatchCommands
    {
        public void Send<T>(T command) where T : class, ICommand
        {
            var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            
            Task.Factory.StartNew(() =>
            {
                var findCriteria = new FindCriteria(typeof (IMessageDistributor));
                findCriteria.Extensions.Add(new XElement(command.Topic));

                var findResponse = discoveryClient.Find(findCriteria);

                findResponse.Endpoints.ToList().ForEach(endpoint =>
                {
                    var service = new ChannelFactory<IMessageDistributor>("MessageDistributor").CreateChannel(endpoint.Address);
                    
                    service.Send(command);
                });
            });
            
        }
    }
}
