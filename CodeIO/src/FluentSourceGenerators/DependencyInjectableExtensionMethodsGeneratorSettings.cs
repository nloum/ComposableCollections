using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    public class DependencyInjectableExtensionMethodsGeneratorSettings
    {
        [XmlAttribute("InterfaceToDependencyInject")]
        public string InterfaceToDependencyInject { get; set; }
        [XmlAttribute("TypeToAddExtensionMethodsFor")]
        public string TypeToAddExtensionMethodsFor { get; set; }
    }
}