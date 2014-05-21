using System;

namespace LoanBook.CustomerRelationshipManagement.Infrastructure.Query
{
    using System.Collections.Generic;

    using LoanBook.CustomerRelationshipManagement.Core.Query;

    public sealed class CustomerQueries : IQueryCustomers
    {
        public IEnumerable<Duplicate> FindDuplicates(DuplicateSearchCriteria duplicateSearchCriteria)
        {
            return new List<Duplicate>();
        }
    }
}
