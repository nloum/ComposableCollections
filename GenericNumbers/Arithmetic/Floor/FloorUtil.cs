using System;

namespace GenericNumbers.Arithmetic.Floor
{
    public static class FloorUtil<T, TOutput>
    {
        static FloorUtil()
        {
            Floor = (arg1) =>
            {
                TOutput output;
                ((IFloor<TOutput>) arg1!).Floor(out output);
                return output;
            };
        }

        public static Func<T, TOutput> Floor { get; set; }
    }
}