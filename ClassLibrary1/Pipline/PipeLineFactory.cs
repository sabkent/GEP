using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ClassLibrary1.Pipline
{
    class PipeLineFactory<T>
    {
        internal PipeLine<T> Create<T>()
        {
            ReadXml();

            List<IFilter<T>> filters = new List<IFilter<T>>
            {
                Activator.CreateInstance(Type.GetType("ClassLibrary1.Pipline.StepOne")) as IFilter<T>
            };



            return new PipeLine<T>(filters);
        }

        private void ReadXml()
        {
            List<IFilter<T>> filters = new List<IFilter<T>>();

            var document = XDocument.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("ClassLibrary1.Pipline.pipeline.xml")));

            foreach(var desc in document.Root.Descendants())
            {
                filters.Add(Activator.CreateInstance(Type.GetType(desc.Attribute("Type").Value)) as IFilter<T>);
            }
            
        }
    }
}
