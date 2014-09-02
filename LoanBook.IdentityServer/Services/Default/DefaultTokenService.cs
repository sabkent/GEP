using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Services.Default
{
    public class DefaultTokenService : ITokenService
    {
        public Task<Connect.Models.Token> CreateIdentityTokenAsync(System.Security.Claims.ClaimsPrincipal claimsPrincipal, Models.Client client, IEnumerable<Models.Scope> scopes, bool includeAllIdentityClaims, System.Collections.Specialized.NameValueCollection parameters, string accessTokenToHash = null)
        {
            throw new NotImplementedException();
        }

        public Task<Connect.Models.Token> CreateAccessTokenAsync(System.Security.Claims.ClaimsPrincipal claimsPrincipal, Models.Client client, IEnumerable<Models.Scope> scopes, System.Collections.Specialized.NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateSecurityTokenAsync(Connect.Models.Token token)
        {
            throw new NotImplementedException();
        }
    }
}
