using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class ConstructorToExtensionMethodGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("ExtensionMethodName")]
        public string ExtensionMethodName { get; set; }
        [XmlAttribute("BaseClass")]
        public string BaseClass { get; set; }
    }
}