using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LoanBook.IdentityServer.Hosting;

namespace LoanBook.IdentityServer.Configuration
{
    public class WebApiConfig
    {
        public static HttpConfiguration Configure()
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.SuppressDefaultHostAuthentication();

            httpConfiguration.MessageHandlers.Insert(0, new KatanaDependencyResolver());

            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            return httpConfiguration;
        }
    }
}
