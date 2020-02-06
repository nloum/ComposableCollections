using GenericNumbers.Relational.GreaterThan;
using GenericNumbers.Relational.LessThan;

namespace GenericNumbers.Relational
{
    public static class RelationExt
    {
        public static int CompareTo<T, TInput>(this T t, TInput input)
        {
            if (t.LessThan(input))
                return -1;
            if (t.GreaterThan(input))
                return 1;
            return 0;
        }
    }
}
