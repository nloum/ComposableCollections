using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class SubclassCombinationImplementationGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("BaseClass")]
        public string BaseClass { get; set; }
        [XmlArray("ClassNameModifiers")]
        [XmlArrayItem("ClassNameModifier", typeof(ClassNameBuilder))]
        public List<ClassNameBuilder> ClassNameModifiers { get; set; }
    }
}