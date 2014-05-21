namespace LoanBook.Correspondence.Api.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using LoanBook.Correspondence.Core;
    using LoanBook.Infrastructure;

    public class EmailsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var documentStore = new MongoDbDocumentStore();

            documentStore.Save(new EmailMessage());

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
