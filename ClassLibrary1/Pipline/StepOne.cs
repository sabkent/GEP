using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Pipline
{
    public class StepOne : IFilter<PipeLineContext>
    {
        public object Execute(PipeLineContext context)
        {
            return null;
        }
    }
}
