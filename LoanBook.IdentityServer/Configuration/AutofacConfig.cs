using Autofac;
using Autofac.Integration.WebApi;
using LoanBook.IdentityServer.Connect.Endpoints;
using LoanBook.IdentityServer.Connect.ResponseHandling;
using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Services.Default;
using LoanBook.IdentityServer.Validation;
using LoanBook.IdentityServer.Views.Embedded;

namespace LoanBook.IdentityServer.Configuration
{
    public static class AutofacConfig
    {
        public static IContainer Configure(IdentityServerOptions identityServerOptions)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(identityServerOptions).AsSelf();

            var authenticationOptions = identityServerOptions.AuthenticationOptions ?? new AuthenticationOptions();
            containerBuilder.RegisterInstance(authenticationOptions).AsSelf();

            containerBuilder.RegisterType<ScopeStore>().As<IScopeStore>();
            containerBuilder.RegisterType<InMemClientStore>().As<IClientStore>();
            containerBuilder.RegisterType<EmbeddedAssetsViewService>().As<IViewService>();
            containerBuilder.RegisterType<DefaultTokenService>().As<ITokenService>();
            containerBuilder.RegisterType<AuthorizeRequestValidator>();
            containerBuilder.RegisterType<AuthorizeInteractionResponseGenerator>();
            containerBuilder.RegisterType<AuthorizeResponseGenerator>();
            containerBuilder.RegisterType<EmbeddedAssetsViewServiceConfiguration>();
            containerBuilder.RegisterType<AuthenticationOptions>();

            containerBuilder.RegisterApiControllers(typeof (DiscoveryEndpointController).Assembly);
            return containerBuilder.Build();
        }
    }
}