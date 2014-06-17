
using Microsoft.Owin;

[assembly: OwinStartup(typeof(LoanBook.LoanManagementSystem.Startup))]
namespace LoanBook.LoanManagementSystem
{
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });
            
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                                                   {
                                                       Client_Id = "loanbook",
                                                       Authority = "http://localhost:3333/core",
                                                       Redirect_Uri = "http://localhost:22177/",
                                                       Response_Type = "id_token token",
                                                       Scope = "openid",
                                                       SignInAsAuthenticationType = "Cookies",
                                                   });
        }
    }
}