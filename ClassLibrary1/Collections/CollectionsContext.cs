using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using ClassLibrary1.Collections.Entity;

namespace ClassLibrary1.Collections
{
    public class CollectionsContext : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionAttempt> CollectionAttempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CollectionAttempConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        class CollectionAttempConfiguration : EntityTypeConfiguration<CollectionAttempt>
        {
            public CollectionAttempConfiguration()
            {
                HasKey(x => new {x.DebtId, x.Attempt});
            }
        }
    }
}
