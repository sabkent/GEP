using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LoanBook.Collections.Core;
using LoanBook.Collections.Core.Data;

namespace LoanBook.Collections.Infrastructure.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        public void Add(Payment payment)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["main"].ConnectionString))
            {
                connection.Open();
                connection.Execute("INSERT [dbo].[Payment](Id, DueDate, Amount) VALUES(@Id, @DueDate, @Amount)", payment);
            }
        }
    }
}
