using System;

namespace GenericNumbers.Arithmetic.SpecialNumbers
{
    public static class SpecialNumbersUtil<T>
    {
        static SpecialNumbersUtil()
        {
            IsNegativeInfinite = (arg1) =>
            {
                bool output;
                ((ISpecialNumbers)arg1).IsNegativeInfinite(out output);
                return output;
            };
            IsPositiveInfinite = (arg1) =>
            {
                bool output;
                ((ISpecialNumbers)arg1).IsPositiveInfinite(out output);
                return output;
            };
            IsNumber = (arg1) =>
            {
                bool output;
                ((ISpecialNumbers)arg1).IsNumber(out output);
                return output;
            };
        }

        public static Func<T, bool> IsNegativeInfinite { get; set; }
        public static Func<T, bool> IsPositiveInfinite { get; set; }
        public static Func<T, bool> IsNumber { get; set; }
    }
}