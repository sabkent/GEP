using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IPublishEvents
    {
        void Publish<T>(T @event);
    }

    public interface IDispatchCommands
    {
        void Dispatch<T>(T command);
    }

    public class ContainerMessageBus : IPublishEvents, IDispatchCommands
    {

        public void Publish<T>(T @event)
        {
            throw new NotImplementedException();
        }

        public void Dispatch<T>(T command)
        {
            throw new NotImplementedException();
        }
    }
}
