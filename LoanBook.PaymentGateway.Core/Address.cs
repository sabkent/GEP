using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.PaymentGateway.Core
{
    public class Address
    {
        public string City { get; set; }
        public string CountryIsoCode { get; set; }
        public string LineOne { get; set; }
        public string PostalCode { get; set; }
        public string RegionState { get; set; }
    }
}
