namespace GenericNumbers.Arithmetic.Times
{
    public interface ITimes<in TInput, TOutput>
    {
        void Times(TInput input, out TOutput output);
    }

    public interface ITimes<T> : ITimes<T, T>
    {
        
    }
}