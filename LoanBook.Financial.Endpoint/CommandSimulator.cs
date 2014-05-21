using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Endpoint;
using NServiceBus;
using LoanBook.Financial.Messaging.Events;

namespace LoanBook.Financial.Endpoint
{
    class CommandSimulator : IWantToRunWhenBusStartsAndStops
    {
        private IBus _bus;

        public CommandSimulator(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            new CommandInterpreter(_bus, new Dictionary<string, Action<IBus, string>>
            {
                {"arrears", SendAccountInArrears},
                {"settle", SendAccountSettled}
            }).Run();
        }

        public void Stop()
        {
            
        }

        private void SendAccountInArrears(IBus bus, string args)
        {
            Guid id;
            if(Guid.TryParse(args, out id))
                bus.Publish(new AccountInArrears{Id = id});
        }

        private void SendAccountSettled(IBus bus, string args)
        {
            Guid id;
            if(Guid.TryParse(args, out id))
                bus.Publish(new AccountSettled(id));
        }
    }
}
