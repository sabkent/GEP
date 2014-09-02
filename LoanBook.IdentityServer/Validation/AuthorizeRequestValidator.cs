using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Extensions;
using LoanBook.IdentityServer.Models;
using LoanBook.IdentityServer.Services;

namespace LoanBook.IdentityServer.Validation
{
    public class AuthorizeRequestValidator
    {
        private readonly IClientStore _clientStore;
        private readonly ValidatedAuthorizeRequest _validatedAuthorizeRequest;

        public AuthorizeRequestValidator(IClientStore clientStore)
        {
            _clientStore = clientStore;
            _validatedAuthorizeRequest = new ValidatedAuthorizeRequest();
        }

        public ValidatedAuthorizeRequest ValidatedAuthorizeRequest
        {
            get { return _validatedAuthorizeRequest; }
        }

        public ValidationResult ValidateProtocol(NameValueCollection parameters)
        {
            _validatedAuthorizeRequest.Parameters = parameters;

            var clientId = parameters.Get(Constants.AuthorizeRequest.ClientId);
            if (clientId.IsMissing())
                return Invalid();

            _validatedAuthorizeRequest.ClientId = clientId;

            var redirectUri = parameters.Get(Constants.AuthorizeRequest.RedirectUri);
            if (redirectUri.IsMissing())
                return Invalid();

            Uri uri;
            if (Uri.TryCreate(redirectUri, UriKind.Absolute, out uri) == false)
                return Invalid();

            _validatedAuthorizeRequest.RedirectUri = new Uri(redirectUri);

            var responseType = parameters.Get(Constants.AuthorizeRequest.ResponseType);
            if (responseType.IsMissing())
                return Invalid();

            if (Constants.SupportedResponseTypes.Contains(responseType) == false)
                return Invalid();

            _validatedAuthorizeRequest.ResponseType = responseType;

            if (_validatedAuthorizeRequest.ResponseType == Constants.ResponseTypes.Code)
            {
                _validatedAuthorizeRequest.Flow = Flows.Code;
                _validatedAuthorizeRequest.ResponseMode = Constants.ResponseModes.Query;
            }
            else if(_validatedAuthorizeRequest.ResponseType == Constants.ResponseTypes.Token ||
                     _validatedAuthorizeRequest.ResponseType == Constants.ResponseTypes.IdToken ||
                    _validatedAuthorizeRequest.ResponseType == Constants.ResponseTypes.IdTokenToken)
            {
                _validatedAuthorizeRequest.Flow = Flows.Implicit;
                _validatedAuthorizeRequest.ResponseMode = Constants.ResponseModes.Fragment;
            }

            var scope = parameters.Get(Constants.AuthorizeRequest.Scope);
            if (scope.IsMissing())
                return Invalid();

            scope = scope.Trim();

            _validatedAuthorizeRequest.IsOpenIdRequest = scope.Contains(Constants.StandardScopes.OpenId);
            _validatedAuthorizeRequest.RequestedScopes = scope.Split(' ').Distinct().ToList();

            if (_validatedAuthorizeRequest.ResponseType != Constants.ResponseTypes.Code)
            {
                if (_validatedAuthorizeRequest.IsOpenIdRequest)
                {
                    if (_validatedAuthorizeRequest.ResponseType.Contains(Constants.ResponseTypes.IdToken) == false)
                        return Invalid();
                }
                else
                {
                    if (_validatedAuthorizeRequest.ResponseType != Constants.ResponseTypes.Token)
                        return Invalid();
                }
            }

            return Valid();
        }

        private ValidationResult Invalid()
        {
            return new ValidationResult
            {
                IsError = true,
                Error = ""
            };
        }

        private ValidationResult Valid()
        {
            return new ValidationResult
            {
                IsError = false
            };
        }

        internal async Task<ValidationResult> ValidateClientAsync()
        {
            if(_validatedAuthorizeRequest.ClientId.IsMissing())
                throw new InvalidOperationException("Client id is empty");

            var client = await _clientStore.FindClientByIdAsync(_validatedAuthorizeRequest.ClientId);
            if (client == null || client.Enabled == false)
                return Invalid();

            _validatedAuthorizeRequest.Client = client;

            return new ValidationResult();
        }
    }
}
