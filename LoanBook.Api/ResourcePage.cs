using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Api
{
    public class ResourcePage<T>
    {
        public Link Previous { get; set; }
        public Link Next { get; set; }
    }
}
