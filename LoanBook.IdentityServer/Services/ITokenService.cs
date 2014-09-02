using LoanBook.IdentityServer.Connect.Models;
using LoanBook.IdentityServer.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Services
{
    public interface ITokenService
    {
        Task<Token> CreateIdentityTokenAsync(ClaimsPrincipal claimsPrincipal, Client client, IEnumerable<Scope> scopes, bool includeAllIdentityClaims, NameValueCollection parameters, string accessTokenToHash = null);
        Task<Token> CreateAccessTokenAsync(ClaimsPrincipal claimsPrincipal, Client client, IEnumerable<Scope> scopes, NameValueCollection parameters);
        Task<string> CreateSecurityTokenAsync(Token token);
    }
}