namespace GenericNumbers.Arithmetic.Minus
{
    public interface IMinus<in TInput, TOutput>
    {
        void Minus(TInput input, out TOutput output);
    }

    public interface IMinus<T> : IMinus<T, T>
    {
    }
}