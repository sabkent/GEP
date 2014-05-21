using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.CustomerRelationshipManagement.Core.Query
{
    using LoanBook.CustomerRelationshipManagement.Core.Query.Projections;

    public interface IQueryCustomers
    {
        IEnumerable<Duplicate> FindDuplicates(DuplicateSearchCriteria duplicateSearchCriteria);
    }
}
