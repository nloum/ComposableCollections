namespace GenericNumbers.Arithmetic.RaisedTo
{
    public interface IRaisedTo<in TInput, TOutput>
    {
        void RaisedTo(TInput input, out TOutput output);
    }

    public interface IRaisedTo<T> : IRaisedTo<T, T>
    {
    }
}