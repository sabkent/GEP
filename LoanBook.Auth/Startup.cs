using LoanBook.Auth;
using LoanBook.Auth.Config;
using LoanBook.IdentityServer.Configuration;
using LoanBook.IdentityServer.Configuration.AppBuilderExtensions;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace LoanBook.Auth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", coreApp =>
            {
                var factory = CreateFactory();
                var identityServerOptions = new IdentityServerOptions
                {
                    IssuerUri = "https://auth.loanbook.com",
                    Factory = factory,
                    PublicHostName = "http://localhost:2493/",
                    SigningCertificate = Cert.Load(),
                    SiteName = "your mom"
                };

                coreApp.UseIdentityServer(identityServerOptions);
            });
        }

        private IdentityServerServiceFactory CreateFactory()
        {
            return new IdentityServerServiceFactory();
        }
    }
}