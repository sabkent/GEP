
using Microsoft.Owin;

[assembly: OwinStartup(typeof(LoanBook.LoanManagementSystem.Startup))]
namespace LoanBook.LoanManagementSystem
{
    using Microsoft.Owin.Security.Cookies;

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
        }
    }
}