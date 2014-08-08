using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Origination.Core;

namespace LoanBook.Origination.Endpoint
{
    public class OriginationContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
    }
}
