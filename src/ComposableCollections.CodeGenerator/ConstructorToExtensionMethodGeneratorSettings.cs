using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class ConstructorToExtensionMethodGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("ExtensionMethodName")]
        public string ExtensionMethodName { get; set; }
        [XmlArray("BaseClasses")]
        [XmlArrayItem("BaseClass", typeof(string))]
        public List<string> BaseClasses { get; set; }
    }
}