using LoanBook.IdentityServer.Extensions;
using Owin;

namespace LoanBook.IdentityServer.Configuration.AppBuilderExtensions
{
    internal static class ConfigureIdentityServerBaseUrlExtension
    {
        public static IAppBuilder ConfigureIdentityServerBaseUrl(this IAppBuilder app, string publicHostName)
        {
            app.Use(async (ctx, next) =>
            {
                var baseUrl = ctx.Environment.GetBaseUrl(publicHostName);
                ctx.Environment.SetIdentityServerBaseUrl(baseUrl);
                await next();
            });

            return app;
        }
    }
}
