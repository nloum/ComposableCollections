using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    [XmlRoot("FluentApiSourceGenerator")]
    public class Configuration
    {
        [XmlArray("CodeGenerators")]
        [XmlArrayItem("AnonymousImplementation", typeof(AnonymousImplementationGeneratorSettings))]
        [XmlArrayItem("CombinationInterfaces", typeof(CombinationInterfacesGeneratorSettings))]
        [XmlArrayItem("ConstructorToExtensionMethod", typeof(ConstructorToExtensionMethodGeneratorSettings))]
        [XmlArrayItem("DecoratorBase", typeof(DecoratorBaseGeneratorSettings))]
        [XmlArrayItem("SubclassCombinationImplementation", typeof(SubclassCombinationImplementationGeneratorSettings))]
        public List<object> CodeGenerators { get; set; } = new List<object>();
    }
}