using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class ExtensionMethodVariationGeneratorSettings
    {
        [XmlAttribute("ExtensionMethodName")]
        public string ExtensionMethodName { get; set; }
    }
}