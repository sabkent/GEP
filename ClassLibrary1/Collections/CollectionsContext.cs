using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Collections.Entity;

namespace ClassLibrary1.Collections
{
    public class CollectionsContext : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionAttempt> CollectionAttempts { get; set; }
    }
}
