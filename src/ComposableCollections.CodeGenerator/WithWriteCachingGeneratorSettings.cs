using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class WithWriteCachingGeneratorSettings
    { 
        [XmlArray("Interfaces")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> Interfaces { get; set; }
    }
}