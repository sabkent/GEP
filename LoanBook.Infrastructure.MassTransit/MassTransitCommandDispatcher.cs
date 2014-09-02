using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using MassTransit;

namespace LoanBook.Infrastructure.MassTransit
{
    public class MassTransitCommandDispatcher : IDispatchCommands
    {

        public MassTransitCommandDispatcher()
        {
            Bus.Initialize(bus =>
            {
                bus.UseRabbitMq(co => co.ConfigureHost(new Uri("rabbitmq://rabbitmq.burgerama.co.uk/seb/two"), configurator =>
                {
                    configurator.SetPassword("Brussels12");
                    configurator.SetUsername("skent");
                }));

                bus.ReceiveFrom("rabbitmq://dstockhammer:P0rter64@rabbitmq.burgerama.co.uk/seb/two");
            });
        }

        public void Send<T>(T command) where T : class, ICommand
        {
            Bus.Instance.Publish(command);
        }
    }

    public class MasCOmmand : ICommand
    {

        public string Topic
        {
            get { return String.Empty; }
        }
    }

    public class all : Consumes<MasCOmmand>.Context
    {
        public void Consume(MasCOmmand message)
        {
            bool pass = true;
        }

        public void Consume(IConsumeContext<MasCOmmand> message)
        {
            bool pass = true;
        }
    }
}
