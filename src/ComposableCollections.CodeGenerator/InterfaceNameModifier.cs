using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComposableCollections.CodeGenerator
{
    public class InterfaceNameModifier : IEnumerable<string>
    {
        [XmlArray]
        [XmlArrayItem("Value")]
        public List<string> Values { get; set; } = new List<string>();

        public void Add(string value)
        {
            Values.Add(value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }
}