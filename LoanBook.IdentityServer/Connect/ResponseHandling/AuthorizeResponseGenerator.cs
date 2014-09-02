using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Connect.Models;
using LoanBook.IdentityServer.Models;
using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Validation;

namespace LoanBook.IdentityServer.Connect.ResponseHandling
{
    public class AuthorizeResponseGenerator
    {
        private readonly ITokenService _tokenService;

        public AuthorizeResponseGenerator(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<AuthorizeResponse> CreateCodeFlowResponseAsync(ValidatedAuthorizeRequest validatedAuthorizeRequest, ClaimsPrincipal claimsPrincipal)
        {
            var authorizationCode = new AuthorizationCode
            {

            };

            var id = Guid.NewGuid().ToString("N");

            return new AuthorizeResponse
            {
                RedirectUri = validatedAuthorizeRequest.RedirectUri,
                Code = id
            };
        }

        public async Task<AuthorizeResponse> CreateImplicitFlowResponseAsync(ValidatedAuthorizeRequest validatedAuthorizeRequest)
        {
            string accessTokenValue = null;
            int accessTokenLifetime = 0;

            if (validatedAuthorizeRequest.IsResourceRequest)
            {
                var accessToken = await _tokenService.CreateAccessTokenAsync(validatedAuthorizeRequest.Subject, validatedAuthorizeRequest.Client, new List<Scope>(), validatedAuthorizeRequest.Parameters);
            }

            string jwt = null;
            if (validatedAuthorizeRequest.IsOpenIdRequest)
            {
                var idToken = await _tokenService.CreateIdentityTokenAsync(validatedAuthorizeRequest.Subject, validatedAuthorizeRequest.Client, new List<Scope>(), validatedAuthorizeRequest.AccessTokenRequested, validatedAuthorizeRequest.Parameters, accessTokenValue);
                jwt = await _tokenService.CreateSecurityTokenAsync(idToken);
            }

            return new AuthorizeResponse
            {
                RedirectUri = validatedAuthorizeRequest.RedirectUri,
                AccessToken = accessTokenValue,
                AccessTokenLifetime = accessTokenLifetime,
                IdentityToken = jwt,
                State = validatedAuthorizeRequest.State,

            };
        }
    }
}
