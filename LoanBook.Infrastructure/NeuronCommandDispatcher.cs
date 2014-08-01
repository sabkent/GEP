using LoanBook.Messaging;
using Neuron.Esb;
using System.Configuration;

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

        public void Send<T>(T command) where T : class, ICommand
        {
            var message = new ESBMessage(command.Topic, command);
            message.Header.BodyType = typeof (T).AssemblyQualifiedName;
            _party.SendMessage(message);
        }
    }
}