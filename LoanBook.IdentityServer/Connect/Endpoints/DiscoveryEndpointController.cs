using System;
using System.Collections.Generic;
using LoanBook.IdentityServer.Configuration;
using LoanBook.IdentityServer.Connect.Models;
using LoanBook.IdentityServer.Extensions;
using LoanBook.IdentityServer.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoanBook.IdentityServer.Connect.Endpoints
{
    public class DiscoveryEndpointController : ApiController
    {
        private readonly IdentityServerOptions _identityServerOptions;
        private readonly IScopeStore _scopeStore;

        public DiscoveryEndpointController(IdentityServerOptions identityServerOptions, IScopeStore scopeStore)
        {
            _identityServerOptions = identityServerOptions;
            _scopeStore = scopeStore;
        }

        [Route(Constants.RoutePaths.Oidc.DiscoveryConfiguration)]
        public async Task<IHttpActionResult> GetConfiguration()
        {
            var baseUrl = Request.GetIdentityServerBaseUrl();
            var scopes = await _scopeStore.GetScopesAsync();

            return Json(new
            {
                issuer = _identityServerOptions.IssuerUri,
                jwks_uri = baseUrl + Constants.RoutePaths.Oidc.DiscoveryWebKeys,
                authorization_endpoint = baseUrl + Constants.RoutePaths.Oidc.Authorize,
                token_endpoint = baseUrl + Constants.RoutePaths.Oidc.Token,
                userinfo_endpoint = baseUrl + Constants.RoutePaths.Oidc.UserInfo,
                end_session_endpoint = baseUrl + Constants.RoutePaths.Oidc.EndSession,
                scopes_supported = scopes.Select(s => s.Name),
                response_types_supported = Constants.SupportedResponseTypes,
                response_modes_supported = Constants.SupportedResponseModes,
                grant_types_supported = Constants.SupportedGrantTypes,
                subject_types_support = new[] {"pairwise", "public"},
                id_token_signing_alg_values_supported = "RS256"
            });
        }

        [Route(Constants.RoutePaths.Oidc.DiscoveryWebKeys)]
        public IHttpActionResult GetKeyData()
        {
            var webKeys = new List<JsonWebKeyDto>();

            foreach (var pubKey in _identityServerOptions.PublicKeysForMetadata)
            {
                if (pubKey != null)
                {
                    var cert64 = Convert.ToBase64String(pubKey.RawData);
                    var thumbprint = Base64Url.Encode(pubKey.GetCertHash());

                    var webKey = new JsonWebKeyDto
                    {
                        kty = "RSA",
                        use = "sig",
                        kid = thumbprint,
                        x5t = thumbprint,
                        x5c = new[] {cert64}
                    };

                    webKeys.Add(webKey);
                }
            }

            return Json(new {keys = webKeys});

        }
    }
}
