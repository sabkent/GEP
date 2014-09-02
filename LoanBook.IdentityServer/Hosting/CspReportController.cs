using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LoanBook.IdentityServer.Configuration;

namespace LoanBook.IdentityServer.Hosting
{
    public class CspReportController : ApiController
    {
        private readonly IdentityServerOptions _identityServerOptions;

        public CspReportController(IdentityServerOptions identityServerOptions)
        {
            _identityServerOptions = identityServerOptions;
        }

        [Route(Constants.RoutePaths.CspReport, Name=Constants.RouteNames.CspReport)]
        public async Task<IHttpActionResult> Post()
        {
            return Ok();
        }
    }
}
