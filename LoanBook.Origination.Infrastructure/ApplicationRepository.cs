using System;

namespace LoanBook.Origination.Infrastructure
{
    using System.Configuration;
    using System.Data.SqlClient;

    using Dapper;

    using LoanBook.Origination.Core;

    public sealed class ApplicationRepository : IApplicationRepository
    {
        public Application GetById(Guid id)
        {
            return new Application();
        }

        public void Save(Application application)
        {
            try
            {
                using (
                    var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["main"].ConnectionString))
                {
                    connection.Open();
                    connection.Execute(
                        "INSERT [dbo].[Application](ApplicationId, Submitted, Amount, FirstName, LastName) VALUES(@Id, GETDATE(), @Amount, @FirstName, @LastName)",
                        application);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
