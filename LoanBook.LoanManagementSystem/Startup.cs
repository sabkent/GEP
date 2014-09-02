using Autofac;
using LoanBook.Messaging;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using System.Diagnostics;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(LoanBook.LoanManagementSystem.Startup))]
namespace LoanBook.LoanManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureContainer(app);

            var hubConfiguration = new HubConfiguration
            {
                Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container)
            };
            app.MapSignalR(hubConfiguration);
            GlobalHost.DependencyResolver = hubConfiguration.Resolver;
            var signalrContainerUpdate = new ContainerBuilder();
            signalrContainerUpdate.RegisterInstance(hubConfiguration.Resolver.GetService(typeof(IConnectionManager))).As<IConnectionManager>();
            signalrContainerUpdate.Update(container);

            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            httpConfiguration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
            app.UseWebApi(httpConfiguration);
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            
            
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            //                                       {
            //                                           Client_Id = "implicitclient",
            //                                           Client_Secret = "secret",
            //                                           Authority = "http://localhost:2493/core", // "http://localhost:3333/core",
            //                                           Redirect_Uri = "http://localhost:22177/",
            //                                           Response_Type = "id_token token",
            //                                           Scope = "openid email read",
            //                                           SignInAsAuthenticationType = "Cookies",
                                                       
            //                                           Notifications = new OpenIdConnectAuthenticationNotifications
            //                                           {
            //                                               MessageReceived = async n =>
            //                                                   {
            //                                                       var token = n.ProtocolMessage.Token;
            //                                                       Debug.WriteLine("message received: " + n.ProtocolMessage.Id_Token);
            //                                                       if (!string.IsNullOrEmpty(token))
            //                                                       {
            //                                                           n.OwinContext.Set("idsrv:token", token);
            //                                                       }
            //                                                   },
            //                                               AccessCodeReceived = async notification =>
            //                                               {
            //                                                   Debug.WriteLine("Access code recived: "+ notification.Code);
            //                                               },
            //                                               SecurityTokenReceived = async notification =>
            //                                               {
            //                                                   Debug.WriteLine("security token received: "+ notification.SecurityToken);
            //                                               },
            //                                               SecurityTokenValidated = async notification =>
            //                                               {
            //                                                   Debug.WriteLine("Security token validated");
            //                                               },
            //                                            }
            //                                       });


            
        }

    }
}