using System.Collections.Generic;
using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    [XmlRoot("FluentApiSourceGenerator")]
    public class Configuration
    {
        [XmlArray("CodeGenerators")]
        [XmlArrayItem("AnonymousImplementations", typeof(AnonymousImplementationsGeneratorSettings))]
        [XmlArrayItem("CombinationInterfaces", typeof(CombinationInterfacesGeneratorSettings))]
        [XmlArrayItem("ConstructorExtensionMethods", typeof(ConstructorExtensionMethodsGeneratorSettings))]
        [XmlArrayItem("DecoratorBaseClasses", typeof(DecoratorBaseClassesGeneratorSettings))]
        [XmlArrayItem("SubclassCombinationImplementations", typeof(SubclassCombinationImplementationsGeneratorSettings))]
        [XmlArrayItem("DependencyInjectableExtensionMethods", typeof(DependencyInjectableExtensionMethodsGeneratorSettings))]
        public List<object> CodeGenerators { get; set; } = new List<object>();
    }
}