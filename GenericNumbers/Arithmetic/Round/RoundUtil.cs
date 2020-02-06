using System;

namespace GenericNumbers.Arithmetic.Round
{
    public static class RoundUtil<T, TOutput>
    {
        static RoundUtil()
        {
            Round = (arg1) =>
            {
                TOutput output;
                ((IRound<TOutput>) arg1).Round(out output);
                return output;
            };
        }

        public static Func<T, TOutput> Round { get; set; }
    }
}