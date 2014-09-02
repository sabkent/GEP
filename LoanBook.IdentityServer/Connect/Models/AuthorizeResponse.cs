using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Connect.Models
{
    public class AuthorizeResponse
    {
        public string Code { get; set; }

        public Uri RedirectUri { get; set; }

        public string AccessToken { get; set; }

        public int AccessTokenLifetime { get; set; }

        public string IdentityToken { get; set; }

        public string State { get; set; }
    }
}
