using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class AnonymousImplementationGeneratorSettings
    {
        [XmlArray("InterfacesToImplement")]
        [XmlArrayItem("Interface", typeof(string))]
        public List<string> InterfacesToImplement { get; set; }
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlArray("AllowedArguments")]
        [XmlArrayItem("AllowedArgument", typeof(string))]
        public List<string> AllowedArguments { get; set; }

        [XmlAttribute("MaxMemberCountPassedIndividually")]
        public int MaxMemberCountPassedIndividually { get; set; } = 1;
        [XmlAttribute("CachePropertyValues")]
        public bool CachePropertyValues { get; set; }
    }
}