using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class WithReadWriteLockGeneratorSettings
    {
        [XmlArray("Interfaces")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> Interfaces { get; set; }
    }
}