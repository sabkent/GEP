﻿namespace LoanBook.Correspondence.Api.Models
{
    public class SendEmailRequest
    {
        public string[] To { get; set; }

        public string Subject{ get; set; }

        public string Body { get; set; }
    }
}