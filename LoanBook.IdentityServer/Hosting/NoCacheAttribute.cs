using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace LoanBook.IdentityServer.Hosting
{
    internal class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            if (actionExecutedContext != null && actionExecutedContext.Response != null &&
                actionExecutedContext.Response.IsSuccessStatusCode)
            {
                var cacheControlHeader = new CacheControlHeaderValue
                {
                    NoStore = true,
                    NoCache = true,
                    Private = true,
                    MaxAge = TimeSpan.Zero
                };

                actionExecutedContext.Response.Headers.CacheControl = cacheControlHeader;
                actionExecutedContext.Response.Headers.Add("Pragma", "no-cache");
            }
        }
    }
}
