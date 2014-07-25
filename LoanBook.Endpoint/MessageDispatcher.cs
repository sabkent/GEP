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

            MethodInfo dispatchMethod = null;
            if (typeof (ICommand).IsAssignableFrom(messageType))
            {
                dispatchMethod = this.GetType()
                        .GetMethod("DispatchCommand", BindingFlags.Instance | BindingFlags.NonPublic)
                        .MakeGenericMethod(messageType);
            }

            if (typeof (IEvent).IsAssignableFrom(messageType))
            {
                dispatchMethod = this.GetType()
                        .GetMethod("DispatchEvent", BindingFlags.Instance | BindingFlags.NonPublic)
                        .MakeGenericMethod(messageType);
            }

            if(dispatchMethod != null)
                dispatchMethod.Invoke(this, new object[] { e.Message });
        }

        private void DispatchCommand<T>(ESBMessage esbMessage)
        {
            var message = esbMessage.GetBody<T>();
            var handlerType = typeof(IHandleCommand<>).MakeGenericType(typeof(T));
            var handler = _componentContext.Resolve(handlerType);

            ((dynamic)handler).Handle(message);
        }

        private void DispatchEvent<T>(ESBMessage esbMessage)
        {
            var message = esbMessage.GetBody<T>();
            var handlers = _componentContext.Resolve<IEnumerable<ISubscribeToEvent<T>>>().ToList();
            handlers.ForEach(subscriber => subscriber.Notify(message));
        }
    }
}
