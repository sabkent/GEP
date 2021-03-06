﻿using System.Configuration;
using LoanBook.Collections.Messaging.Commands;
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
            var url = ConfigurationManager.AppSettings["paymentServiceUrl"];

            ViewBag.IFrameUrl = String.Format("{0}/cardentry/savecard/{1}", url, Guid.NewGuid());

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
            _dispatchCommands.Send(new StartDebtCollections());
        }

        public void SignLoan(Guid applicationId)
        {
         
        }
        

        public void SendRegisterCashPaymentCommand()
        {
            //_dispatchCommands.Send(new RegisterCashPayment());
        }
    }
}