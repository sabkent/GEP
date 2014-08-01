using System.Data.Entity.ModelConfiguration;
using LoanBook.Collections.Core.Entities;
using System.Data.Entity;

namespace LoanBook.Collections.Endpoint
{
    public class CollectionsContext : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CollectionEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class CollectionEntityConfiguration : EntityTypeConfiguration<Collection>
    {
        public CollectionEntityConfiguration()
        {
            HasKey(x => x.DebtId);
        }
    }
}
