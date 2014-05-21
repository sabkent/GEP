using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanBook.Collections.Messaging.Commands;
using LoanBook.Origination.Messaging.Commands;

namespace LoanBook.LoanManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
    }
}