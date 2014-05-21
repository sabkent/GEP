namespace LoanBook.Correspondence.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using LoanBook.Correspondence.Api.Models;

    public class AdHocEmailsController : ApiController
    {
        public HttpResponseMessage Get(Guid id)
        {
            var emailSendingReport = new EmailSendingReport { Status = EmailSentStatus.Processing };
            
            return Request.CreateResponse(HttpStatusCode.OK, emailSendingReport);
            //can return a body stating, Still processing
            //or the representation here is always a SendEmailTaskReport. with status of pending | failed | complete
        }

        public HttpResponseMessage Post(SendEmailRequest sendEmailRequest)
        {
            var taskId = Guid.NewGuid().ToString("N");

            var response = Request.CreateResponse(HttpStatusCode.Accepted);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "AdHocEmails", id = taskId }));
            
            return response;
        }
    }
}
