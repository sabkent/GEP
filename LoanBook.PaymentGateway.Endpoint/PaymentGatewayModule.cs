using Autofac;
using LoanBook.PaymentGateway.Core;
using LoanBook.PaymentGateway.Infrastructure.PaymentGateways.Paypal;

namespace LoanBook.PaymentGateway.Endpoint
{
    public class PaymentGatewayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaypalPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PaymentGatewayContext>();
            base.Load(builder);
        }
    }
}
