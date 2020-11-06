using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class InterfaceNameModifier
    {
        [XmlArray("OneOf")]
        [XmlArrayItem("Part", typeof(string))]
        public List<string> Values { get; set; }
    }
}