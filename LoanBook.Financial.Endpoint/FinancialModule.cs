namespace LoanBook.Financial.Endpoint
{
    using Autofac;
    using Core;
    using Infrastructure;

    internal class FinancialModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
        }
    }
}
