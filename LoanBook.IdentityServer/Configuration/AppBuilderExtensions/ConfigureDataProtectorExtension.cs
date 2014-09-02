using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace LoanBook.IdentityServer.Configuration.AppBuilderExtensions
{
    internal static class ConfigureDataProtectorExtension
    {
        public static IAppBuilder ConfigureDataProtectionProvider(this IAppBuilder app, IdentityServerOptions identityServerOptions)
        {
            if (identityServerOptions.DataProtector == null)
            {
                var provider = app.GetDataProtectionProvider() ?? new DpapiDataProtectionProvider(Constants.PrimaryAuthenticationType);
                
                identityServerOptions.DataProtector = new HostDataProtector(provider);
            }
            return app;
        }
    }
}
