using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LoanBook.IdentityServer.Configuration;
using LoanBook.IdentityServer.Extensions;
using LoanBook.IdentityServer.Hosting;
using LoanBook.IdentityServer.Views.ActionResults;
using LoanBook.IdentityServer.Views.ViewModels;
using LoanBook.IdentityServer.Services;

namespace LoanBook.IdentityServer.Authentication
{
    [SecurityHeaders, NoCache]
    public class AuthenticationController : ApiController 
    {
        private readonly IViewService _viewService;
        private readonly IdentityServerOptions _identityServerOptions;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthenticationController(IViewService viewService, IdentityServerOptions identityServerOptions, AuthenticationOptions authenticationOptions)
        {
            _viewService = viewService;
            _identityServerOptions = identityServerOptions;
            _authenticationOptions = authenticationOptions;
        }

        [Route(Constants.RoutePaths.Login, Name= Constants.RouteNames.Login), HttpGet]
        public async Task<IHttpActionResult> Login([FromUri] string message = null)
        {
            if (message != null)
            {
                var signInMessage = SaveSignInMessage(message);
                if (signInMessage.IdP.IsPresent())
                    return Redirect(Url.Link(Constants.RouteNames.LoginExternal, new {provider = signInMessage.IdP}));
            }
            else
            {
                VerifySignInMessage();
            }

            return await RenderLoginPage();
        }

        private SignInMessage SaveSignInMessage(string message)
        {
            var signInMessage = SignInMessage.Unprotect(message, _identityServerOptions.DataProtector);

            var context = Request.GetOwinContext();

            context.Response.Cookies.Append(_identityServerOptions.CookieOptions.Prefix + Constants.SignInMessageCookieName, message, new Microsoft.Owin.CookieOptions
            {
                HttpOnly = true,
                Secure = Request.RequestUri.Scheme == Uri.UriSchemeHttps
            });

            return signInMessage;
        }

        private void VerifySignInMessage()
        {
            var context = Request.GetOwinContext();
            var message = context.Request.Cookies[_identityServerOptions.CookieOptions.Prefix + Constants.SignInMessageCookieName];

            if (message.IsMissing())
            {
                throw new Exception("SignInMessage cookie is empty.");
            }

            try
            {
                SignInMessage.Unprotect(message, _identityServerOptions.DataProtector);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IHttpActionResult> RenderLoginPage(string errorMessage = null, string username = null)
        {
            var context = Request.GetOwinContext();

            var providers = from p in context.Authentication.GetAuthenticationTypes(d => d.Caption.IsPresent())
                select new LoginPageLink {Text = p.Caption, Href=Url.Route(Constants.RouteNames.LoginExternal, new{provider = p.AuthenticationType})};

            var loginViewModel = new LoginViewModel
            {
                SiteName = _identityServerOptions.SiteName,
                SiteUrl = context.Environment.GetIdentityServerBaseUrl(),
                CurrentUser = await GetNameFromPrimaryAuthenticationType(),
                ExternalProviders = providers,
                AdditionsLinks = _authenticationOptions.LoginPageLinks,
                ErrorMessage = errorMessage,
                LoginUrl = Url.Route(Constants.RouteNames.Login, null),
                UserName = username
            };

            return new LoginActionResult(_viewService, context.Environment, loginViewModel);
        }

        private async Task<string> GetNameFromPrimaryAuthenticationType()
        {
            var user = await GetIdentityFromPrimaryAuthenticationType();
            if (user != null)
                return user.Claims.GetValue(Constants.ClaimTypes.Subject);
            return null;
        }

        private async Task<ClaimsIdentity> GetIdentityFromPrimaryAuthenticationType()
        {
            return await GetIdentityFrom(Constants.PrimaryAuthenticationType);
        }

        private async Task<ClaimsIdentity> GetIdentityFrom(string type)
        {
            var context = Request.GetOwinContext();
            var authenticateResult = await context.Authentication.AuthenticateAsync(type);
            return authenticateResult.IsAuthenticated() ? authenticateResult.Identity : null;
        }

       
    }
    internal static class AuthenticationResultExtension
    {
        public static bool IsAuthenticated(this Microsoft.Owin.Security.AuthenticateResult authenticateResult)
        {
            return authenticateResult != null && authenticateResult.Identity != null &&
                authenticateResult.Identity.IsAuthenticated;
        }
    }
}
