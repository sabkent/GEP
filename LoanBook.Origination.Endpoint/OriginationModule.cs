namespace LoanBook.Origination.Endpoint
{
    using Autofac;

    using LoanBook.Origination.Core;
    using LoanBook.Origination.Infrastructure;

    public class OriginationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationRepository>().As<IApplicationRepository>();
            builder.RegisterType<OriginationContext>().SingleInstance();

            builder.RegisterType<ExperianService>().As<IBureauService>();
        }
    }
}
