using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;

namespace LoanBook.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbDocumentStore>().As<IDocumentStore>();
            builder.RegisterType<NeuronCommandDispatcher>().As<IDispatchCommands>();
            builder.RegisterType<NeuronEventPublisher>().As<IPublishEvents>();
        }
    }
}
