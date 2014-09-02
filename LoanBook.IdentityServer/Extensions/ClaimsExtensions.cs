using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LoanBook.IdentityServer.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetValue(this IEnumerable<Claim> claims, string type)
        {
            if (claims != null)
            {
                var claim = claims.SingleOrDefault(x => x.Type == type);
                if (claim != null)
                    return claim.Value;
            }
            return null;
        }
    }
}
