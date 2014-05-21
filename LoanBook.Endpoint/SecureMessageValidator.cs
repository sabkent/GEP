using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Endpoint
{
    using LoanBook.Messaging;
    using NServiceBus;

    public class SecureMessageValidator : IHandleMessages<INeedAuthentication>, ISpecifyMessageHandlerOrdering
    {
        public void Handle(INeedAuthentication message)
        {
            string authenticationHeader = this.Bus().GetMessageHeader(message, "Authorization");
        }

        public void SpecifyOrder(Order order)
        {
            order.SpecifyFirst<SecureMessageValidator>();
        }
    }
}
