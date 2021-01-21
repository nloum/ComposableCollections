using System;

namespace DebuggableSourceGenerators
{
    public class Property
    {
        public Property(string name, Lazy<IType> type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public Lazy<IType> Type { get; }

        // TODO - are the getter / setter public, private, or protected?
        
        public override string ToString()
        {
            return $"{Type.Value} {Name} {{ get; }} {{ set; }}";
        }
    }
}