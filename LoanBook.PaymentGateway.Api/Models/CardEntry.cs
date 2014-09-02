using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanBook.PaymentGateway.Api.Models
{
    public class CardEntry
    {
        public Guid ApplicationId { get; set; }

        public string Number { get; set; }
    }
}