using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Hosting;
using Owin;

namespace LoanBook.IdentityServer.Configuration.AppBuilderExtensions
{
    public static class UseIdentityServerExtension
    {
        public static IAppBuilder UseIdentityServer(this IAppBuilder app, IdentityServerOptions identityServerOptions)
        {
            app.ConfigureDataProtectionProvider(identityServerOptions);

            app.ConfigureIdentityServerBaseUrl(identityServerOptions.PublicHostName);

            app.Use<AutofacContainerMiddleware>(AutofacConfig.Configure(identityServerOptions));
            
            app.UseWebApi(WebApiConfig.Configure());

            return app;
        }
    }
}
