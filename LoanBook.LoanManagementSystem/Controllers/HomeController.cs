using LoanBook.Financial.Messaging.Commands;
using LoanBook.Messaging;

namespace LoanBook.LoanManagementSystem.Controllers
{
    using System;
    using System.Web.Mvc;
    using Thinktecture.IdentityModel.Client;

    public class HomeController : Controller
    {
        private readonly IDispatchCommands _dispatchCommands;

        public HomeController(IDispatchCommands dispatchCommands)
        {
            _dispatchCommands = dispatchCommands;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string cot)
        {
            var client = new OAuth2Client(new Uri("http://localhost:3333/core/connect/authorize"));
            var url = client.CreateCodeFlowUrl("codeclient", "openid offline_access email", "http://localhost:22177/callback/", "123");

            return this.Redirect(url);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Secured()
        {
            return View();
        }

        public void SubmitApplication()
        {
         
        }

        public void CollectDebt()
        {
         
        }

        public void SignLoan(Guid applicationId)
        {
         
        }
        

        public void SendRegisterCashPaymentCommand()
        {
            _dispatchCommands.Send(new RegisterCashPayment());
        }
    }
}