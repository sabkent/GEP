using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LoanBook.LoanManagementSystem.Controllers.Api
{
    using LoanBook.Origination.Messaging.Commands;

    public class OriginationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { FirstName = "bob" });
        }

        public HttpResponseMessage Post(Origination origination)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            

            return response;
        }
    }

    public class Origination
    {
        public Guid Id { get; set; }
    }
}
