using System;

namespace GenericNumbers.Arithmetic.Ceiling
{
    public static class CeilingUtil<T, TOutput>
    {
        static CeilingUtil()
        {
            Ceiling = (arg1) =>
            {
                TOutput output;
                ((ICeiling<TOutput>) arg1).Ceiling(out output);
                return output;
            };
        }

        public static Func<T, TOutput> Ceiling { get; set; }
    }
}