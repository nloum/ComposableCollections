using System;

namespace DebuggableSourceGenerators
{
    public class TypeIdentifier
    {
        public TypeIdentifier(string namespaceName, string name, int arity)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                FullName = $"{namespaceName}.{name}";
            }
            else
            {
                FullName = name;
            }
            
            Arity = arity;
        }

        public TypeIdentifier(string fullName, int arity)
        {
            FullName = fullName;
            Arity = arity;
        }

        public string FullName { get; }
        public int Arity { get; }

        protected bool Equals(TypeIdentifier other)
        {
            return FullName == other.FullName && Arity == other.Arity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeIdentifier) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Arity);
        }

        public override string ToString()
        {
            if (Arity == 0)
            {
                return FullName;
            }
            
            return $"{FullName}`{Arity}";
        }
    }
}