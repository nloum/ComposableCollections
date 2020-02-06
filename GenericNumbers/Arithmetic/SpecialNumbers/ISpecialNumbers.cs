namespace GenericNumbers.Arithmetic.SpecialNumbers
{
    public interface ISpecialNumbers
    {
        void IsNegativeInfinite(out bool output);
        void IsPositiveInfinite(out bool output);
        void IsNumber(out bool output);
    }
}