namespace GenericNumbers.Arithmetic.Plus
{
    public interface IPlus<in TInput, TOutput>
    {
        void Plus(TInput input, out TOutput output);
    }

    public interface IPlus<T> : IPlus<T, T>
    {
    }
}