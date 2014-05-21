using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Correspondence.Infrastructure.MailServers
{
    using LoanBook.Correspondence.Core;

    public interface IMailServerProvider
    {
        IEnumerable<EmailMessage> GetMessages();
    }
}
