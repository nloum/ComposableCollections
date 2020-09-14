using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class DecoratorBaseGeneratorSettings
    {
        [XmlArray("InterfacesToImplement")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> InterfacesToImplement { get; set; }
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
    }
}