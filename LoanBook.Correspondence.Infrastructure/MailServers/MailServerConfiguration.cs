using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Correspondence.Infrastructure.MailServers
{
    public class MailServerConfiguration
    {
        public string Host { get; set; }

        public string MailServerProviderType { get; set; } //Fully qualified type to Activator.CreateInstance'd  or research other plugin architecture
    }
}
