using System.Collections.Generic;
using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    public class AnonymousImplementationsGeneratorSettings
    {
        [XmlArray("InterfacesToImplement")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> InterfacesToImplement { get; set; }
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlArray("AllowedArguments")]
        [XmlArrayItem("AllowedArgument", typeof(string))]
        public List<string> AllowedArguments { get; set; }
        [XmlAttribute("CachePropertyValues")]
        public bool CachePropertyValues { get; set; }
    }
}