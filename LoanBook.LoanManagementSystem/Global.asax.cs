using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace LoanBook.LoanManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IBus Bus { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bus = Configure.With()
                    .DefaultBuilder()
                    .UnicastBus()
                    .DefiningCommandsAs(t=>t.Namespace != null && t.Namespace.EndsWith("Commands"))
                    .CreateBus()
                    .Start(() => Configure.Instance.ForInstallationOn<Windows>());
        }
    }
}
