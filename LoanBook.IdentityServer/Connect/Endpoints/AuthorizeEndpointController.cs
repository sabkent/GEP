using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LoanBook.IdentityServer.Authentication;
using LoanBook.IdentityServer.Configuration;
using LoanBook.IdentityServer.Connect.Models;
using LoanBook.IdentityServer.Connect.ResponseHandling;
using LoanBook.IdentityServer.Connect.Results;
using LoanBook.IdentityServer.Extensions;
using LoanBook.IdentityServer.Hosting;
using LoanBook.IdentityServer.Models;
using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Validation;
using LoanBook.IdentityServer.Views.ActionResults;
using LoanBook.IdentityServer.Views.ViewModels;

namespace LoanBook.IdentityServer.Connect.Endpoints
{
    [HostAuthentication(Constants.PrimaryAuthenticationType)]
    [SecurityHeaders, NoCache]
    public class AuthorizeEndpointController : ApiController
    {
        private readonly IViewService _viewService;
        private readonly AuthorizeRequestValidator _authorizeRequestValidator;
        private readonly AuthorizeInteractionResponseGenerator _authorizeInteractionResponseGenerator;
        private readonly IdentityServerOptions _identityServerOptions;
        private readonly AuthorizeResponseGenerator _authorizeResponseGenerator;

        public AuthorizeEndpointController(IViewService viewService, AuthorizeRequestValidator authorizeRequestValidator, AuthorizeInteractionResponseGenerator authorizeInteractionResponseGenerator, IdentityServerOptions identityServerOptions, AuthorizeResponseGenerator authorizeResponseGenerator)
        {
            
            _viewService = viewService;
            _authorizeRequestValidator = authorizeRequestValidator;
            _authorizeInteractionResponseGenerator = authorizeInteractionResponseGenerator;
            _identityServerOptions = identityServerOptions;
            _authorizeResponseGenerator = authorizeResponseGenerator;
        }

        [Route(Constants.RoutePaths.Oidc.Authorize, Name=Constants.RouteNames.Oidc.Authorize)]
        public async Task<IHttpActionResult> Get(HttpRequestMessage httpRequestMessage)
        {
            return await ProcessRequestAsync(httpRequestMessage.RequestUri.ParseQueryString());
        }

        protected async Task<IHttpActionResult> ProcessRequestAsync(NameValueCollection parameters, UserConsent userConsent = null)
        {
            var validationResult = _authorizeRequestValidator.ValidateProtocol(parameters);
            var validatedAuthorizeRequest = _authorizeRequestValidator.ValidatedAuthorizeRequest;

            if (validationResult.IsError)
            {
                return AuthorizeError(validationResult.ErrorType,
                    validationResult.Error,
                    validatedAuthorizeRequest.ResponseMode, 
                    validatedAuthorizeRequest.RedirectUri,
                    validatedAuthorizeRequest.State);
            }

            var loginInteractionResponse = _authorizeInteractionResponseGenerator.ProcessLogin(validatedAuthorizeRequest, User as ClaimsPrincipal);

            if (loginInteractionResponse.IsError)
                return AuthorizeError(loginInteractionResponse.AuthorizeError);

            if (loginInteractionResponse.IsLogin)
                return RedirectToLogin(loginInteractionResponse.SignInMessage, validatedAuthorizeRequest.Parameters, _identityServerOptions);

            if(User.Identity.IsAuthenticated == false)
                throw new InvalidOperationException("User is not authenticated");

            validatedAuthorizeRequest.Subject = User as ClaimsPrincipal;

            validationResult = await _authorizeRequestValidator.ValidateClientAsync();

            return await CreateAuthorizationResponseAsync(validatedAuthorizeRequest);
        }

        private IHttpActionResult AuthorizeError(ErrorTypes errorType, string error, string responseMode, Uri errorUri, string state)
        {
            return AuthorizeError(new AuthorizeError
            {
                ErrorType = errorType,
                Error = error,
                ResponseMode = responseMode,
                ErrorUri = errorUri,
                State = state
            });
        }

        private IHttpActionResult AuthorizeError(AuthorizeError authorizeError)
        {
            if (authorizeError.ErrorType == ErrorTypes.User)
            {
                var environment = Request.GetOwinEnvironment();
                var errorViewModel = new ErrorViewModel
                {
                    SiteUrl = environment.GetIdentityServerBaseUrl()
                };
                return new ErrorActionResult(_viewService, environment, errorViewModel);
            }
            
            string character;
            if (authorizeError.ResponseMode == Constants.ResponseModes.Query ||
                authorizeError.ResponseMode == Constants.ResponseModes.FormPost)
            {
                character = "?";
            }
            else
            {
                character = "#";
            }

            var url = String.Format("{0}{1}error={2}", authorizeError.ErrorUri.AbsoluteUri, character, authorizeError.Error);
            return Redirect(url);
        }

        private IHttpActionResult RedirectToLogin(SignInMessage signInMessage, NameValueCollection parameters, IdentityServerOptions identityServerOptions)
        {
            signInMessage = signInMessage ?? new SignInMessage();

            var path = Url.Route(Constants.RouteNames.Oidc.Authorize, null) + "?" + parameters.ToQueryString();
            var url = new Uri(Request.RequestUri, path);
            signInMessage.ReturnUrl = url.AbsoluteUri;

            return new LoginResult(signInMessage, Request.GetOwinContext().Environment, identityServerOptions.DataProtector);
        }

        private async Task<IHttpActionResult> CreateAuthorizationResponseAsync(ValidatedAuthorizeRequest validatedAuthorizeRequest)
        {
            if (validatedAuthorizeRequest.Flow == Flows.Implicit)
                return await CreateImplicitFlowAuthorizeResponseAsync(validatedAuthorizeRequest);

            if (validatedAuthorizeRequest.Flow == Flows.Code)
                return await CreateCodeFlowAuthorizeResponseAsync(validatedAuthorizeRequest);

            throw new InvalidOperationException("invalid flow");
        }

        private async Task<IHttpActionResult> CreateImplicitFlowAuthorizeResponseAsync(ValidatedAuthorizeRequest validatedAuthorizeRequest)
        {
            var response = await _authorizeResponseGenerator.CreateImplicitFlowResponseAsync(validatedAuthorizeRequest);
            return new AuthorizeImplicitFormPostResult(Request);
        }

        private async Task<IHttpActionResult> CreateCodeFlowAuthorizeResponseAsync(ValidatedAuthorizeRequest validatedAuthorizeRequest)
        {
            return new AuthorizeImplicitFormPostResult(Request);
        }

        
    }
}
