using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class InterfaceNameBuilder
    {
        [XmlAttribute("SearchRegex")]
        public string Search { get; set; }
        [XmlAttribute("ReplaceRegex")]
        public string Replacement { get; set; }
    }
}