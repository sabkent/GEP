using LoanBook.LoanManagementSystem.Hubs;
using LoanBook.LoanManagementSystem.Models;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Web.Http;

namespace LoanBook.LoanManagementSystem.Controllers.Api
{
    public class ApplicationController : ApiController
    {
        private readonly IConnectionManager _connectionManager;

        public ApplicationController(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        public IHttpActionResult Post(ApplicationSubmission applicationSubmission)
        {
            var applicationHub = _connectionManager.GetHubContext<Application>();

            applicationHub.Clients.All.applicationAccepted();
            return Ok();
        }

        [HttpPost, Route("api/application/{id}/status")]
        public IHttpActionResult Status(Guid id)
        {
            var applicationHub = _connectionManager.GetHubContext<Application>();

            applicationHub.Clients.Group(id.ToString()).applicationSubmissionAccepted(id);

            return Ok();
        }

        [HttpPost, Route("api/application/{id}/card")]
        public IHttpActionResult Card([FromUri]Guid id)
        {
            var applicationHub = _connectionManager.GetHubContext<Application>();

            applicationHub.Clients.Group(id.ToString()).cardSaved(id);

            return Ok();
        }
    }
}
