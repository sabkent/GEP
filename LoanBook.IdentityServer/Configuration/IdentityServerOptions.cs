using LoanBook.Auth.Config;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace LoanBook.IdentityServer.Configuration
{
    public class IdentityServerOptions
    {
        public IdentityServerOptions()
        {
            CookieOptions = new CookieOptions();
        }
        public string IssuerUri { get; set; }
        public IdentityServerServiceFactory Factory { get; set; }

        public string PublicHostName { get; set; }

        public IDataProtector DataProtector { get; set; }

        public X509Certificate2 SigningCertificate { get; set; }

        public CookieOptions CookieOptions { get; set; }

        internal IEnumerable<X509Certificate2> PublicKeysForMetadata
        {
            get
            {
                var keys = new List<X509Certificate2>();

                if(SigningCertificate != null)
                    keys.Add(SigningCertificate);

                return keys;
            }
        }

        public string SiteName { get; set; }
        public AuthenticationOptions AuthenticationOptions { get; set; }
    }
}
