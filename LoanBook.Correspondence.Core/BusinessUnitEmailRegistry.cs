namespace LoanBook.Correspondence.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class BusinessUnitEmailRegistry
    {
        private Dictionary<string, List<string>> _businessEmailAddresses;

        public BusinessUnitEmailRegistry()
        {
            _businessEmailAddresses = new Dictionary<string, List<string>>
                                          {
                                              {"GBR:MEM", new List<string>{"info@memcapital.com", "complaints@memcaptial.com"}}
                                          };
        }

        public string GetBusinessUnitForEmailAddress(string emailAddress) //BusinessUnit entity in shared lib, pass in EmailMessage entity
        {
            return _businessEmailAddresses
                .Where(v => v.Value.Contains(emailAddress))
                .Select(v => v.Key).FirstOrDefault();
        }
    }
}
