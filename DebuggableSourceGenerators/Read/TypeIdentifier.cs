using System;
using System.Text;

namespace DebuggableSourceGenerators.Read
{
    public record TypeIdentifier
    {
        public static TypeIdentifier Parse(string fullName)
        {
            string @namespace;
            string name;
            int arity = 0;
            
            var split = fullName.Split('`');
            if (split.Length == 2)
            {
                arity = int.Parse(split[1]);
                fullName = split[0];
            }

            var lastIndex = fullName.LastIndexOf('.');
            @name = fullName.Substring(lastIndex + 1);
            @namespace = fullName.Substring(0, lastIndex);

            return new TypeIdentifier()
            {
                Name = name,
                Namespace = @namespace,
                Arity = arity
            };
        }
        
        public static TypeIdentifier Parse(string @namespace, string name)
        {
            int arity = 0;
            
            var split = name.Split('`');
            if (split.Length == 2)
            {
                arity = int.Parse(split[1]);
                name = split[0];
            }

            return new TypeIdentifier()
            {
                Name = name,
                Namespace = @namespace,
                Arity = arity
            };
        }

        private readonly string _name;
        public string Namespace { get; init; }

        public string Name
        {
            get => _name;
            init
            {
                if (value.Contains('`'))
                {
                    throw new InvalidOperationException("Names cannot have ` in them");
                }
                _name = value;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(Name);
            if (Arity > 0)
            {
                result.Append('`');
                result.Append(Arity.ToString());
            }

            if (Namespace != null)
            {
                result.Insert(0, '.');
                result.Insert(0, Namespace);
            }
            
            var resultString = result.ToString();
            return resultString;
        }

        public virtual bool Equals(TypeIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name && Namespace == other.Namespace && Arity == other.Arity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, Namespace, Arity);
        }

        public int Arity { get; init; }
    }
}