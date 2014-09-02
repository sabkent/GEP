using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoanBook.IdentityServer.Views.ActionResults
{
    internal class HtmlStreamActionResult : IHttpActionResult
    {
        private Func<Task<Stream>> _renderFunc;

        public HtmlStreamActionResult(Func<Task<Stream>> renderFunc)
        {
            _renderFunc = renderFunc;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await GetResponseMessage();
        }

        public async Task<HttpResponseMessage> GetResponseMessage()
        {
            var stream = await _renderFunc();

            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("text/html")
            {
                CharSet = Encoding.UTF8.WebName
            };

            return new HttpResponseMessage
            {
                Content = streamContent
            };
        }
    }
}
