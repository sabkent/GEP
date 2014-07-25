using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Collections
{
    /// <summary>
    /// bascialy the response from payment gateway
    /// </summary>
    public class Collection
    {
        public Guid Id { get; set; }
        public Guid DebtId { get; set; }
        public string GatewayReference { get; set; }
        public bool Success { get; set; }
    }
}
