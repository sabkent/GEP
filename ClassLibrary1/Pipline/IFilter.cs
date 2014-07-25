using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1.Pipline
{
    interface IFilter<in T>
    {
        object Execute(T context);
    }
}
