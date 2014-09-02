using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Authentication;
using LoanBook.IdentityServer.Connect.Models;
using LoanBook.IdentityServer.Validation;

namespace LoanBook.IdentityServer.Connect.ResponseHandling
{
    public class AuthorizeInteractionResponseGenerator
    {
        private readonly SignInMessage _signInMessage;


        public AuthorizeInteractionResponseGenerator()
        {
            _signInMessage = new SignInMessage();
        }

        public LoginInteractionResponse ProcessLogin(ValidatedAuthorizeRequest validatedAuthorizeRequest, ClaimsPrincipal claimsPrincipal)
        {

            if (validatedAuthorizeRequest.PromptMode == Constants.PromptModes.Login)
            {
                validatedAuthorizeRequest.Parameters.Remove(Constants.AuthorizeRequest.Prompt);
                return new LoginInteractionResponse
                {
                    SignInMessage = _signInMessage
                };
            }

            if (claimsPrincipal.Identity.IsAuthenticated == false)
            {
                if (validatedAuthorizeRequest.PromptMode == Constants.PromptModes.None)
                {
                    return new LoginInteractionResponse
                    {
                        AuthorizeError = new AuthorizeError()
                    };
                }

                return new LoginInteractionResponse
                {
                    SignInMessage = _signInMessage
                };
            }

            return new LoginInteractionResponse();
        }
    }
}
