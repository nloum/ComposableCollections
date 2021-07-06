namespace GenericNumbers.Arithmetic.Abs
{
    public interface IAbs<TOutput>
    {
        void Abs(out TOutput output);
    }
}