using System.Collections.Generic;
using System.Collections.Specialized;
using LoanBook.IdentityServer.Models;
using System;

namespace LoanBook.IdentityServer.Validation
{
    public class ValidatedAuthorizeRequest
    {
        public NameValueCollection Parameters{ get; set; }

        public string ClientId { get; set; }

        public Uri RedirectUri { get; set; }

        public string ResponseType { get; set; }

        public Flows Flow { get; set; }

        public string ResponseMode { get; set; }

        public bool IsOpenIdRequest { get; set; }

        public List<string> RequestedScopes { get; set; }

        public string State { get; set; }

        public string PromptMode { get; set; }

        public System.Security.Claims.ClaimsPrincipal Subject { get; set; }

        public Client Client { get; set; }

        public bool IsResourceRequest { get; set; }

        public bool AccessTokenRequested { get; set; }
    }
}