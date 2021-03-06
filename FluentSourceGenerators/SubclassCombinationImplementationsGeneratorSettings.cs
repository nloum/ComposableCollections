using System.Collections.Generic;
using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    public class SubclassCombinationImplementationsGeneratorSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlAttribute("BaseClass")]
        public string BaseClass { get; set; }
        [XmlArray("ClassNameModifiers")]
        [XmlArrayItem("ClassNameModifier", typeof(ClassNameBuilder))]
        public List<ClassNameBuilder> ClassNameModifiers { get; set; }
        [XmlArray("ClassNameBlacklist")]
        [XmlArrayItem("ClassName", typeof(string))]
        public List<string> ClassNameBlacklist { get; set; } = new List<string>();
        [XmlArray("ClassNameWhitelist")]
        [XmlArrayItem("ClassName", typeof(string))]
        public List<string> ClassNameWhitelist { get; set; } = new List<string>();
        [XmlAttribute("AllowDifferentTypeParameters")]
        public bool AllowDifferentTypeParameters { get; set; }
    }
}