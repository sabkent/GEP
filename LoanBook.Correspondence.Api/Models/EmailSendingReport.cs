using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanBook.Correspondence.Api.Models
{
    public class EmailSendingReport
    {
        public EmailSentStatus Status { get; set; }
    }

    public enum EmailSentStatus
    {
        Processing,
        Failed,
        Sent
    }
}