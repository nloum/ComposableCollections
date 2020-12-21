using System.Collections.Generic;
using System.Xml.Serialization;

namespace FluentSourceGenerators
{
    public class InterfaceNameModifier
    {
        [XmlArray("OneOf")]
        [XmlArrayItem("Part", typeof(string))]
        public List<string> Values { get; set; }
    }
}