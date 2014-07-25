using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ClassLibrary1.Pipline
{
    [TestFixture]
    class PiplineTests
    {
        [Test]
        public void Test()
        {

            var pipeLineFactory = new PipeLineFactory<PipeLineContext>();
            var pipeLine = pipeLineFactory.Create<PipeLineContext>();
            pipeLine.Execute(new PipeLineContext());

        }
    }

    public class PipeLineContext
    {
        
    }
}
