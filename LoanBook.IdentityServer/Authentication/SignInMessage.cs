using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Configuration;
using Newtonsoft.Json;

namespace LoanBook.IdentityServer.Authentication
{
    public class SignInMessage
    {
        private const string Entropy = "signinmessage";
        public string ReturnUrl { get; set; }

        public DateTime ValidTo { get; set; }

        public string IdP { get; set; }

        internal string Protect(int ttl, IDataProtector dataProtector)
        {
            ValidTo = DateTime.UtcNow.AddSeconds(ttl);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(this, jsonSerializerSettings);

            return dataProtector.Protect(json, Entropy);
        }

        internal static SignInMessage Unprotect(string data, IDataProtector dataProtector)
        {
            var json = dataProtector.Unprotect(data, Entropy);
            var signInMessage = JsonConvert.DeserializeObject<SignInMessage>(json);

            if(DateTime.UtcNow > signInMessage.ValidTo)
                throw new Exception("SiginMessage expired");

            return signInMessage;
        }
    }
}
