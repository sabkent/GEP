using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using Neuron.Esb;

namespace LoanBook.Infrastructure
{
    public sealed class NeuronCommandDispatcher : IDispatchCommands
    {
        private readonly Party _party;

        public NeuronCommandDispatcher()
        {
            var partyName = ConfigurationManager.AppSettings["neuron:publisher"];
            _party = new Party(partyName);
            _party.Connect();
        }

        public void Send<T>(T command) where T : ICommand
        {
            var topic = "Financials";
            var message = new ESBMessage(topic, command);
            message.Header.BodyType = typeof (T).AssemblyQualifiedName;
            _party.SendMessage(message);
        }
    }
}
