namespace LoanBook.Correspondence.Endpoint
{
    using Autofac;

    using LoanBook.Infrastructure;

    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new InfrastructureModule());
            Configure.With().AutofacBuilder(containerBuilder.Build());

            //var mailServices = ConfigurationManager.GetSection("mailServers") as MailServersConfig;

        }
    }
}
