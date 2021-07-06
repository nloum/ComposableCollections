namespace GenericNumbers.Arithmetic.DividedBy
{
    public interface IDividedBy<in TInput, TOutput>
    {
        void DividedBy(TInput input, out TOutput output);
    }

    public interface IDividedBy<T> : IDividedBy<T, T>
    {

    }
}