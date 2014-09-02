using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Autofac.Integration.SignalR;
using LoanBook.Infrastructure;
using Owin;

namespace LoanBook.LoanManagementSystem
{
    public partial class Startup
    {
        public IContainer ConfigureContainer(IAppBuilder app)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterControllers(assembly);
            containerBuilder.RegisterApiControllers(assembly);
            containerBuilder.RegisterHubs(assembly);
            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
            
            return container;
        }
    }
}