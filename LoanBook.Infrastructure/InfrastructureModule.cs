using Autofac;
using LoanBook.Messaging;

namespace LoanBook.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbDocumentStore>().As<IDocumentStore>();
            //builder.RegisterType<NeuronCommandDispatcher>().As<IDispatchCommands>();
            builder.RegisterType<NeuronEventPublisher>().As<IPublishEvents>();
            //builder.RegisterType<NeuronMessageDispatcher>().As<IMessageDispatcher>();


            builder.RegisterType<WcfMessageDispatcher>().As<IMessageDispatcher>();
            builder.RegisterType<WcfCommandDispatcher>().As<IDispatchCommands>();
        }
    }
}
