using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Origination.Core;

namespace LoanBook.Origination.Infrastructure.Bureau
{
    public class ExperianService : IBureauService
    {
        public BureauResponse Check(BureauRequest bureauRequest)
        {
            return new BureauResponse();
        }
    }
}
