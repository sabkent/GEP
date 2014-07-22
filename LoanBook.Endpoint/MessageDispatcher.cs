using System.Collections.Generic;
using System.Linq;
using Autofac;
using LoanBook.Messaging;
using Neuron.Esb;
using System;
using System.Configuration;
using System.Reflection;

namespace LoanBook.Endpoint
{
    public interface IMessageDispatcher
    {
        void Listen();
    }
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _componentContext;
        private Party _party;

        public MessageDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            var partyName = ConfigurationManager.AppSettings["neuron:dispatcher:party"];
            _party = new Party(partyName);
            _party.OnReceive += OnReceive;
        }

        public void Listen()
        {
            _party.Connect();
        }

        private void OnReceive(object sender, MessageEventArgs e)
        {
            var bodyType = e.Message.Header.BodyType;
            var messageType = Type.GetType(bodyType);

            var dispatchMethod =
                this.GetType()
                    .GetMethod("Dispatch", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(messageType);

            dispatchMethod.Invoke(this, new object[] {e.Message});
        }

        private void Dispatch<T>(ESBMessage esbMessage)
        {
            var messageType = typeof (T);
            var message = esbMessage.GetBody<T>();
            if (typeof (ICommand).IsAssignableFrom(messageType))
            {
                var handlerType = typeof(IHandleCommand<>).MakeGenericType(typeof(T));
                var handler = _componentContext.Resolve(handlerType);

                ((dynamic)handler).Handle(message);
            }
            if (typeof (IEvent).IsAssignableFrom(messageType))
            {
                //generioc type contraint fucks us here
                _componentContext.Resolve<IEnumerable<ISubscribeToEvent<T>>>().ToList().ForEach(subscriber => subscriber.Notify(message));
            }
        }
    }
}
