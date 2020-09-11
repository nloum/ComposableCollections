using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.SourceGenerator
{
    [XmlRoot("FluentApiSourceGeneratorConfig")]
    public class FluentApiSourceGeneratorConfig
    {
        [XmlArray("SourceGenerators")]
        [XmlArrayItem("CompositeInterfaceImplementation", Type = typeof(CompositeInterfaceImplementation))]
        public List<IFluentApiSourceGenerator> SourceGenerators { get; set; } = new List<IFluentApiSourceGenerator>();
    }
}
