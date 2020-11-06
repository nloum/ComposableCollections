using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class ClassNameBuilder
    {
        [XmlAttribute("Search")]
        public string Search { get; set; }
        [XmlAttribute("Replace")]
        public string Replace { get; set; }
    }
}