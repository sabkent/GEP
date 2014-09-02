using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Api.Models;
using LoanBook.PaymentGateway.Messaging.Events;

namespace LoanBook.PaymentGateway.Api.Controllers
{
    public class CardController : Controller
    {
        private readonly IPublishEvents _eventsPublisher;

        public CardController(IPublishEvents eventsPublisher)
        {
            _eventsPublisher = eventsPublisher;
        }


        public ActionResult Entry(Guid id)
        {
            return View(new CardEntry {ApplicationId = id});
        }

        [HttpPost]
        public ActionResult Entry(CardEntry cardEntry)
        {
            var cardSavedForApplication = new CardSavedForApplication
            {
                ApplicationId = cardEntry.ApplicationId
            };

            _eventsPublisher.Publish(cardSavedForApplication);
            return View(cardEntry);
        }

        public ActionResult SaveCard(Guid id)
        {
            var cardEntry = new CardEntry {ApplicationId = id};
            return View(cardEntry);
        }

        [HttpPost]
        public ActionResult SaveCard(CardEntry cardEntry)
        {

            var applicationId = cardEntry.ApplicationId;


            var result = Url.Action("SaveCard") + "#success";
            return new RedirectResult(result);
        }
    }
}