using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace LoanBook.Endpoint
{
    public class EndpointModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageDispatcher>().As<IMessageDispatcher>();
        }
    }
}
