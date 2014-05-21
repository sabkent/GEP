using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoanBook.CustomerServices.Site.Models;

namespace LoanBook.CustomerServices.Site.Controllers.Api
{
    public class CustomersController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var customers = new List<Customer>
            {
                new Customer {FirstName = "seabug"},
                new Customer{ FirstName = "deanomite"}
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, customers);
            return response;
        }
    }
}
