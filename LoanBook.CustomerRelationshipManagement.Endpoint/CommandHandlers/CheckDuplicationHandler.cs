using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.CustomerRelationshipManagement.Endpoint.CommandHandlers
{
    using LoanBook.CustomerRelationshipManagement.Core.Query;
    using LoanBook.CustomerRelationshipManagement.Messaging.Commands;
    using LoanBook.CustomerRelationshipManagement.Messaging.Events;

    using NServiceBus;

    public class CheckDuplicationHandler : IHandleMessages<CheckDuplication>
    {
        private readonly IBus bus;

        private readonly IQueryCustomers customerQueries;

        public CheckDuplicationHandler(IBus bus, IQueryCustomers customerQueries)
        {
            this.bus = bus;
            this.customerQueries = customerQueries;
        }

        public void Handle(CheckDuplication checkDuplication)
        {
            Console.WriteLine("Check duplication");

            var duplicates = this.customerQueries.FindDuplicates(new DuplicateSearchCriteria());

            this.bus.Publish(new DuplicationCheckComplete{ApplicationId = checkDuplication.ApplicationId});
        }
    }
}
