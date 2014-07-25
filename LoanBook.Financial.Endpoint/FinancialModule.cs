using System.Reflection;
using LoanBook.Core;
using LoanBook.Messaging;

namespace LoanBook.Financial.Endpoint
{
    using Autofac;
    using Core;
    using Infrastructure;

    internal class FinancialModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = this.GetType().Assembly;
            
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
            builder.RegisterType<DigiaFinancialServicesProvider>().As<IFinancialServicesProvider>();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssiableFromGenericType(typeof (IHandleCommand<>)))
                .AsClosedTypesOf(typeof (IHandleCommand<>));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssiableFromGenericType(typeof(ISubscribeToEvent<>)))
                .AsClosedTypesOf(typeof(ISubscribeToEvent<>));
        }
    }
}
