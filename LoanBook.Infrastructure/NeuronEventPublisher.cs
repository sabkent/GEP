using System.Configuration;
using LoanBook.Messaging;
using System;
using Neuron.Esb;

namespace LoanBook.Infrastructure
{
    public sealed class NeuronEventPublisher : IPublishEvents
    {
        private readonly Party _party;

        public NeuronEventPublisher()
        {
            var partyName = ConfigurationManager.AppSettings["neuron:publisher"];
            _party = new Party(partyName);
            _party.Connect();
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var message = new ESBMessage(@event.Topic, @event);
            message.Header.BodyType = typeof (T).AssemblyQualifiedName;
            _party.SendMessage(message);
        }
    }
}
