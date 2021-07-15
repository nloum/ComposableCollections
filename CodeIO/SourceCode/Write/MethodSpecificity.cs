using System;

namespace CodeIO.SourceCode.Write
{
    public class MethodSpecificity : IComparable<MethodSpecificity>
    {
        public IMethod Method { get; }

        public MethodSpecificity(IMethod method)
        {
            Method = method;
        }

        public int CompareTo(MethodSpecificity? other)
        {
            if (other == null)
            {
                return -1;
            }

            if (Method.ReturnType.IsConvertibleTo(other.Method.ReturnType))
            {
                return -1;
            }

            if (other.Method.ReturnType.IsConvertibleTo(Method.ReturnType))
            {
                return 1;
            }

            return 0;
        }
    }
}