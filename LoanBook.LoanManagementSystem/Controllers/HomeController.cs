using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanBook.Collections.Messaging.Commands;
using LoanBook.Origination.Messaging.Commands;

namespace LoanBook.LoanManagementSystem.Controllers
{
    using LoanBook.LoanManagementSystem.Hubs;

    using Microsoft.AspNet.SignalR;

    using Thinktecture.IdentityModel.Client;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string cot)
        {
            var client = new OAuth2Client(new Uri("http://localhost:3333/core/connect/authorize"));
            var url = client.CreateCodeFlowUrl("loanbook", "openid", "http://localhost:22177/callback/", "123");

            return this.Redirect(url);
        }

        public void SubmitApplication()
        {
            MvcApplication.Bus.Send(new SubmitApplication());
        }

        public void CollectDebt()
        {
            MvcApplication.Bus.Send(new StartDebtCollections());
        }

        public void SignLoan(Guid applicationId)
        {
            MvcApplication.Bus.Send(new SignAgreement {ApplicationId = applicationId});
        }

        public void SignalR()
        {
            GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients.All.callbackFunction();
        }
    }
}