using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Management.Instrumentation;

namespace ClassLibrary1.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Basic> Basics { get; set; }

        public DbSet<LoanAgreement> LoanAgreements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Basic.BasicConfiguration());
            modelBuilder.Configurations.Add(new LoanAgreement.LoanAgreementConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    public partial class Basic
    {
        public Guid Id { get; set; }
        private string Column1 { get; set; }

        internal void DoIt()
        {
            Column1 = "this is private";
        }
    }

    public partial class Basic
    {
        public class BasicConfiguration : EntityTypeConfiguration<Basic>
        {
            public BasicConfiguration()
            {
                Property(x => x.Column1);
            }
        }
    }
}
