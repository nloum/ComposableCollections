using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class InterfaceNameBuilder
    {
        [XmlAttribute("Search")]
        public string Search { get; set; }
        [XmlAttribute("Replace")]
        public string Replace { get; set; }
    }
}