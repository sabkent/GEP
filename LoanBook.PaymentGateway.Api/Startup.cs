using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoanBook.PaymentGateway.Api;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Startup))]
namespace LoanBook.PaymentGateway.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureContainer(app);
        }
    }
}