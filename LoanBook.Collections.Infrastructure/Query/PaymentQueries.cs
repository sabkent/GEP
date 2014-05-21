using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LoanBook.Collections.Core.Query;
using LoanBook.Collections.Core.Query.Projections;

namespace LoanBook.Collections.Infrastructure.Query
{
    public class PaymentQueries : IQueryPayments
    {
        public IEnumerable<DuePayment> GetPaymentDueFor(DateTime dueDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["main"].ConnectionString))
            {
                connection.Open();
                return connection.Query<DuePayment>("SELECT Id, Amount FROM dbo.[Payment] WHERE DueDate=@DueDate",
                    new {DueDate = DateTime.Now.Date});
            }
        }
    }
}
