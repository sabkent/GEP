using LoanBook.Collections.Core.Entities;
using System.Data.Entity;

namespace LoanBook.Collections.Endpoint
{
    public class CollectionsContext : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Collection> Collections { get; set; }
    }
}
