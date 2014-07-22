
using Autofac;
using Autofac.Integration.Mvc;
using LoanBook.Infrastructure;
using Microsoft.Owin;
using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;

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
            ConfigureContainer();
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });
            
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                                                   {
                                                       Client_Id = "implicitclient",
                                                       Client_Secret = "secret",
                                                       Authority = "http://localhost:3333/core",
                                                       Redirect_Uri = "http://localhost:22177/",
                                                       Response_Type = "id_token token",
                                                       Scope = "openid email read",
                                                       SignInAsAuthenticationType = "Cookies",
                                                       
                                                       Notifications = new OpenIdConnectAuthenticationNotifications
                                                       {
                                                           MessageReceived = async n =>
                                                               {
                                                                   var token = n.ProtocolMessage.Token;
                                                                   Debug.WriteLine("message received: " + n.ProtocolMessage.Id_Token);
                                                                   if (!string.IsNullOrEmpty(token))
                                                                   {
                                                                       n.OwinContext.Set("idsrv:token", token);
                                                                   }
                                                               },
                                                           AccessCodeReceived = async notification =>
                                                           {
                                                               Debug.WriteLine("Access code recived: "+ notification.Code);
                                                           },
                                                           SecurityTokenReceived = async notification =>
                                                           {
                                                               Debug.WriteLine("security token received: "+ notification.SecurityToken);
                                                           },
                                                           SecurityTokenValidated = async notification =>
                                                           {
                                                               Debug.WriteLine("Security token validated");
                                                           },
                                                        }
                                                   });
        }

        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = containerBuilder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
        }
    }
}