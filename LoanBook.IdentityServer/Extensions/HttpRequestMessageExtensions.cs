using System.Net.Http;

namespace LoanBook.IdentityServer.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetIdentityServerBaseUrl(this HttpRequestMessage request)
        {
            return request.GetOwinContext().Environment.GetIdentityServerBaseUrl();
        }
    }
}
