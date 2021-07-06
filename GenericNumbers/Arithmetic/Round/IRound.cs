namespace GenericNumbers.Arithmetic.Round
{
    public interface IRound<TOutput>
    {
        void Round(out TOutput output);
    }
}