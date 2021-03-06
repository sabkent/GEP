﻿using Autofac;
using LoanBook.Collections.Core;
using LoanBook.Collections.Core.Data;
using LoanBook.Collections.Core.Query;
using LoanBook.Collections.Infrastructure.Data;
using LoanBook.Collections.Infrastructure.Query;

namespace LoanBook.Collections.Endpoint
{
    class CollectionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentQueries>().As<IQueryPayments>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
            builder.RegisterType<DefaultDebtRescheduler>().As<IRescheduleDebts>();

            builder.RegisterType<CollectionsContext>().SingleInstance();
        }
    }
}
