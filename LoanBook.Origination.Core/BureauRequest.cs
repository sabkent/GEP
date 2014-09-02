using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Origination.Core
{
    public class BureauRequest
    {
    }

    public class BureauResponse
    {
        
    }

    public interface IBureauService
    {
        BureauResponse Check(BureauRequest bureauRequest);
    }
}
