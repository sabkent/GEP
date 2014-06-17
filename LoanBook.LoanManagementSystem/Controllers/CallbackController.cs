﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanBook.LoanManagementSystem.Controllers
{
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    using Thinktecture.IdentityModel;
    using Thinktecture.IdentityModel.Client;

    public class CallbackController : Controller
    {
        // GET: Callback
        public async Task<ActionResult> Index()
        {
            await this.GetToken();
            return View();
        }

        private async Task<string> GetToken()
        {
            var client = new OAuth2Client(new Uri("http://localhost:3333/core/connect/token"), "loanbook", "loanbook_secret");

            var code = Request.QueryString["code"];

            var response = await client.RequestAuthorizationCodeAsync(code, "http://localhost:22177/callback/");

            this.ValidateResponseAndSignIn(response);

            return "";
        }

        private void ValidateResponseAndSignIn(TokenResponse response)
        {
            if (!string.IsNullOrWhiteSpace(response.IdentityToken))
            {
                var claims = ValidateToken(response.IdentityToken);

                if (!string.IsNullOrWhiteSpace(response.AccessToken))
                {
                    claims.Add(new Claim("access_token", response.AccessToken));
                    claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
                }

                if (!string.IsNullOrWhiteSpace(response.RefreshToken))
                {
                    claims.Add(new Claim("refresh_token", response.RefreshToken));
                }

                var id = new ClaimsIdentity(claims, "Cookies");
                Request.GetOwinContext().Authentication.SignIn(id);
            }
        }

        private List<Claim> ValidateToken(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidAudiences = new List<string>{"loanbook"},
                ValidIssuer = "https://idsrv3.com",
                IssuerSigningToken = new X509SecurityToken(X509.LocalMachine.TrustedPeople.SubjectDistinguishedName.Find("CN=idsrv3test", false).First())
            };

            var id = new JwtSecurityTokenHandler().ValidateToken(token, parameters);
            return id.Claims.ToList();
        }

        private string ParseJwt(string token)
        {
            if (!token.Contains("."))
            {
                return token;
            }

            var parts = token.Split('.');
            var part = Encoding.UTF8.GetString(Thinktecture.IdentityModel.Base64Url.Decode(parts[1]));

            var jwt = JObject.Parse(part);
            return jwt.ToString();
        }
    }
}