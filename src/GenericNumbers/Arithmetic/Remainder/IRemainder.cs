namespace GenericNumbers.Arithmetic.Remainder
{
    public interface IRemainder<in TInput, TOutput>
    {
        void Remainder(TInput input, out TOutput output);
    }

    public interface IRemainder<T> : IRemainder<T, T>
    {
        
    }
}