using System;

namespace GenericNumbers.Arithmetic.Sqrt
{
    public static class SqrtUtil<T, TOutput>
    {
        static SqrtUtil()
        {
            Sqrt = (arg1) =>
            {
                TOutput output;
                ((ISqrt<TOutput>) arg1!).Sqrt(out output);
                return output;
            };
        }

        public static Func<T, TOutput> Sqrt { get; set; }
    }
}