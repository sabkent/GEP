using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Core;
namespace LoanBook.CustomerRelationshipManagement.Endpoint
{
    using LoanBook.CustomerRelationshipManagement.Core;
    using LoanBook.CustomerRelationshipManagement.Core.Query;
    using LoanBook.CustomerRelationshipManagement.Infrastructure;
    using LoanBook.CustomerRelationshipManagement.Infrastructure.Query;
    using LoanBook.Messaging;

    internal class CustomerRelationshipManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = this.GetType().Assembly;

            builder.RegisterType<CustomerQueries>().As<IQueryCustomers>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();


            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssiableFromGenericType(typeof(IHandleCommand<>)))
                .AsClosedTypesOf(typeof(IHandleCommand<>));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssiableFromGenericType(typeof(ISubscribeToEvent<>)))
                .AsClosedTypesOf(typeof(ISubscribeToEvent<>));
        }
    }
}
