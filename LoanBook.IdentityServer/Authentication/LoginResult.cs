using LoanBook.IdentityServer.Configuration;
using LoanBook.IdentityServer.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoanBook.IdentityServer.Authentication
{
    public class LoginResult : IHttpActionResult
    {
        private readonly SignInMessage _signInMessage;
        private readonly IDictionary<string, object> _environment;
        private readonly IDataProtector _dataProtector;

        public LoginResult(SignInMessage signInMessage, IDictionary<string, object> environment, IDataProtector dataProtector)
        {
            _signInMessage = signInMessage;
            _environment = environment;
            _dataProtector = dataProtector;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Redirect);
            try
            {
                var sim = _signInMessage.Protect(600, _dataProtector);
                var url = _environment.GetIdentityServerBaseUrl() + Constants.RoutePaths.Login;
                url += "?message=" + sim;

                var uri = new Uri(url);
                httpResponseMessage.Headers.Location = uri;
            }
            catch
            {
                httpResponseMessage.Dispose();
                throw;
            }
            return httpResponseMessage;
        }
    }
}
