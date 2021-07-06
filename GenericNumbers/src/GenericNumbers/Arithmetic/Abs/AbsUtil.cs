using System;

namespace GenericNumbers.Arithmetic.Abs
{
    public static class AbsUtil<T, TOutput>
    {
        static AbsUtil()
        {
            Abs = (arg1) =>
            {
                TOutput output;
                ((IAbs<TOutput>) arg1!).Abs(out output);
                return output;
            };
        }

        public static Func<T, TOutput> Abs { get; set; }
    }
}