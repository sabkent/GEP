namespace LoanBook.Correspondence.Infrastructure.ExchangeMailServerProvider
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using LoanBook.Correspondence.Infrastructure.MailServers;

    using Microsoft.Exchange.WebServices.Data;

    public class MailServerProvider : IMailServerProvider
    {
        public IEnumerable<LoanBook.Correspondence.Core.EmailMessage> GetMessages()
        {
            var service = new ExchangeService(ExchangeVersion.Exchange2007_SP1)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("", ""),
                Url = new Uri("https://mail.dfconline.com/ews/exchange.asmx")
            };
            
            var dateFilter = new SearchFilter.IsGreaterThan(ItemSchema.DateTimeReceived, DateTime.Now.AddDays(-1));
            var items = service.FindItems(WellKnownFolderName.Inbox, dateFilter, new ItemView(10));

            return null;
        }
    }
}
