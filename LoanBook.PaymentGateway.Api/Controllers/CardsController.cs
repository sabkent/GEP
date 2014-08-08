using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoanBook.PaymentGateway.Api.Models;

namespace LoanBook.PaymentGateway.Api.Controllers
{
    public class CardsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(CardEntry cardEntry)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
