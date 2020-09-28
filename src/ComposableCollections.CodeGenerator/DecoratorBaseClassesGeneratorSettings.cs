using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class DecoratorBaseClassesGeneratorSettings
    {
        [XmlArray("InterfacesToImplement")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> InterfacesToImplement { get; set; }
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("Folder")]
        public string Folder { get; set; }
    }
}