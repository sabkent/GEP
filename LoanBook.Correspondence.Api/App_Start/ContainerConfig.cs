namespace LoanBook.Correspondence.Api.App_Start
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using LoanBook.Infrastructure;

    public class ContainerConfig
    {
        public static void Configure()
        {
            var containerBuilder = new ContainerBuilder();

            var executingAssembly = Assembly.GetExecutingAssembly();

            containerBuilder.RegisterApiControllers(executingAssembly);
            containerBuilder.RegisterAssemblyModules(executingAssembly);
            containerBuilder.RegisterModule(new InfrastructureModule());
            var container = containerBuilder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}