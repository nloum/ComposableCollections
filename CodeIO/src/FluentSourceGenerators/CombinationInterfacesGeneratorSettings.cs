using System.Collections.Generic;
using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    public class CombinationInterfacesGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlArray("InterfaceNameModifiers")]
        [XmlArrayItem("InterfaceNameModifier", typeof(InterfaceNameModifier))]
        public List<InterfaceNameModifier> InterfaceNameModifiers { get; set; }
        [XmlArray("InterfaceNameBuilders")]
        [XmlArrayItem("InterfaceNameBuilder", typeof(InterfaceNameBuilder))]
        public List<InterfaceNameBuilder> InterfaceNameBuilders { get; set; }
        [XmlArray("InterfaceNameBlacklist")]
        [XmlArrayItem("Regex", typeof(string))]
        public List<string> InterfaceNameBlacklistRegexes { get; set; }
        [XmlArray("PreExistingInterfaces")]
        [XmlArrayItem("PreExistingInterface", typeof(string))]
        public List<string> PreExistingInterfaces { get; set; }
    }
}