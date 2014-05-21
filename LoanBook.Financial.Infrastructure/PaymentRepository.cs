using System;

namespace LoanBook.Financial.Infrastructure
{
    using System.Configuration;
    using System.Data.SqlClient;
    using Dapper;
    using Core;

    public sealed class PaymentRepository : IPaymentRepository
    {
        public void Add(Payment payment)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["main"].ConnectionString))
                {
                    connection.Open();
                    connection.Execute("INSERT [dbo].[Payment](Id, AccountId, Amount, Due) VALUES(@Id, @AccountId, @Amount, @Due)", payment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
