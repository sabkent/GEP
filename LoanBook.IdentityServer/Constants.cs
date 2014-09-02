using System;
using System.Collections.Generic;

namespace LoanBook.IdentityServer
{
    public static class Constants
    {
        public const string EmptyString = "";
        public const string PrimaryAuthenticationType = "idsrv";
        public const string SignInMessageCookieName = "idsrv.signin.message";

        public static class RoutePaths
        {
            public const string Login = "login";
            public const string CspReport = "csp/report";

            public static class Oidc
            {
                public const string DiscoveryConfiguration = ".well-known/openid-configuration";
                public const string DiscoveryWebKeys = ".well-known/jwks";
                public const string Authorize = "connect/authorize";
                public const string Token = "connect/token";
                public const string UserInfo = "connect/userinfo";
                public const string EndSession = "connect/endsession";
            }
        }

        public static class OwinEnvironment
        {
            public const string IdentityServerBaseUrl = "idsrv:IdentityServerBaseUrl";
            public const string AutofacScope = "idsrv:AutofacScope";
        }

        public static class RouteNames
        {
            public const string Login = "idsrv.authentication.login";
            public const string LoginExternal = "idsrv.authentication.loginexternal";
            public const string CspReport = "idsrv.csp.report";

            public static class Oidc
            {
                public const string Authorize = "idsrv.oidc.authorize";
            }
        }

        public static class AuthorizeRequest
        {
            public const string ClientId = "client_id";
            public const string RedirectUri = "redirect_uri";
            public const string ResponseType = "response_type";
            public const string Scope = "scope";
            public const string Prompt = "prompt";
        }

        public static class ResponseTypes
        {
            public const string Token = "token";
            public const string IdToken = "id_token";
            public const string IdTokenToken = "id_token token";
            public const string Code = "code";
        }

        public static class ResponseModes
        {
            public const string FormPost = "form_post";
            public const string Query = "query";
            public const string Fragment = "fragment";
        }

        public static class StandardScopes
        {
            public const string OpenId = "openid";
            public const string Profile = "profile";
            public const string Email = "email";
            public const string Address = "address";
            public const string Phone = "phone";
            public const string OfflineAccess = "offline_access";
        }

        public static class ClaimTypes
        {
            // core oidc claims
            public const string Subject = "sub";
            public const string Name = "name";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string MiddleName = "middle_name";
            public const string NickName = "nickname";
            public const string PreferredUserName = "preferred_username";
            public const string Profile = "profile";
            public const string Picture = "picture";
            public const string WebSite = "website";
            public const string Email = "email";
            public const string EmailVerified = "email_verified";
            public const string Gender = "gender";
            public const string BirthDate = "birthdate";
            public const string ZoneInfo = "zoneinfo";
            public const string Locale = "locale";
            public const string PhoneNumber = "phone_number";
            public const string PhoneNumberVerified = "phone_number_verified";
            public const string Address = "address";
            public const string Audience = "aud";
            public const string Issuer = "iss";
            public const string NotBefore = "nbf";
            public const string Expiration = "exp";

            // more standard claims
            public const string UpdatedAt = "updated_at";
            public const string IssuedAt = "iat";
            public const string AuthenticationMethod = "amr";
            public const string AuthenticationContextClassReference = "acr";
            public const string AuthenticationTime = "auth_time";
            public const string AuthorizedParty = "azp";
            public const string AccessTokenHash = "at_hash";
            public const string AuthorizationCodeHash = "c_hash";
            public const string Nonce = "nonce";

            // more claims
            public const string ClientId = "client_id";
            public const string Scope = "scope";
            public const string Id = "id";
            public const string Secret = "secret";
            public const string IdentityProvider = "idp";

            // claims for authentication controller partial logins
            public const string AuthorizationReturnUrl = "authorization_return_url";
            public const string PartialLoginReturnUrl = "partial_login_return_url";
        }

        public static class PromptModes
        {
            public const string None = "none";
            public const string Login = "login";
            public const string Consent = "consent";
            public const string SelectAccount = "select_account";
        }

        public static class GrantTypes
        {
            public const string Password = "password";
            public const string AuthorizationCode = "authorization_code";
            public const string ClientCredentials = "client_credentials";
            public const string RefreshToken = "refresh_token";
            public const string Implicit = "implicit";

            // assertion grants
            public const string Saml2Bearer = "urn:ietf:params:oauth:grant-type:saml2-bearer";
            public const string JwtBearer = "urn:ietf:params:oauth:grant-type:jwt-bearer";
        }

        public static readonly List<string> SupportedResponseTypes = new List<string>
        {
            ResponseTypes.Code,
            ResponseTypes.Token,
            ResponseTypes.IdToken,
            ResponseTypes.IdTokenToken
        };

        public static readonly List<string> SupportedResponseModes = new List<string>
        {
            ResponseModes.FormPost,
            ResponseModes.Query,
            ResponseModes.Fragment
        };

        public static readonly List<string> SupportedGrantTypes = new List<string>
        {
            GrantTypes.AuthorizationCode,
            GrantTypes.ClientCredentials,
            GrantTypes.Password,
            GrantTypes.Implicit
        };
    }
}