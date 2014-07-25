using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1.Pipline
{
    /// <summary>
    /// http://eventuallyconsistent.net/2013/09/24/branching-messages-with-pipelines/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class PipeLine<T>
    {
        private List<IFilter<T>> _filters;

        public PipeLine(List<IFilter<T>> filters)
        {
            _filters = filters;
        }


        public void Execute(T context)
        {
            _filters.ForEach(filter=>filter.Execute(context));
        }
    }
}
