using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoanBook.IdentityServer.Connect.Results
{
    internal sealed class AuthorizeImplicitFormPostResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public AuthorizeImplicitFormPostResult(HttpRequestMessage httpRequestMessage)
        {
            _httpRequestMessage = httpRequestMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var virtualRootPath = _httpRequestMessage.GetRequestContext().VirtualPathRoot;


            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("your mom")
            };

            return httpResponseMessage;
        }
    }
}
