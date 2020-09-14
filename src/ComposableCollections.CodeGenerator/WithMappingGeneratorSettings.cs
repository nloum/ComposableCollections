using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class WithMappingGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("Class")]
        public string Class { get; set; }
        [XmlAttribute("Partial")]
        public bool Partial { get; set; }
        [XmlArray("Interfaces")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> Interfaces { get; set; }
    }
}