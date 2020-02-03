namespace SimpleMonads
{
    public interface IMaybe<out T> : IMonad<T>, IMaybe
    {
        T Value { get; }
    }

    public interface IMaybe
    {
        bool HasValue { get; }
        object ObjectValue { get; }
    }
}