using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using LoanBook.Infrastructure;
using Owin;

namespace LoanBook.PaymentGateway.Api
{
	public partial class Startup
	{
        public IContainer ConfigureContainer(IAppBuilder app)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterControllers(assembly);
            
            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            return container;
        }
	}
}