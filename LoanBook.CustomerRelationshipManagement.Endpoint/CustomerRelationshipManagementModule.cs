using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.CustomerRelationshipManagement.Endpoint
{
    using LoanBook.CustomerRelationshipManagement.Core;
    using LoanBook.CustomerRelationshipManagement.Core.Query;
    using LoanBook.CustomerRelationshipManagement.Infrastructure;
    using LoanBook.CustomerRelationshipManagement.Infrastructure.Query;

    internal class CustomerRelationshipManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerQueries>().As<IQueryCustomers>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
        }
    }
}
