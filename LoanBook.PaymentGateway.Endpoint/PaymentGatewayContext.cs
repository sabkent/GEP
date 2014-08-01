using System.Data.Entity;
using LoanBook.PaymentGateway.Core;

namespace LoanBook.PaymentGateway.Endpoint
{
    public class PaymentGatewayContext : DbContext
    {
        public DbSet<CardHolder> CardHolders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.ComplexType<Card>();
            modelBuilder.ComplexType<Address>();
            base.OnModelCreating(modelBuilder);
        }
    }
}