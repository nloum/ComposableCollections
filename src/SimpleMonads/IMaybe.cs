namespace SimpleMonads
{
    /// <summary>
    ///     Represents either a value of type T or nothing at all.
    /// </summary>
    /// <typeparam name="T">The type of value that this maybe may contain</typeparam>
    public interface IMaybe<out T> : IMonad<T>, IMaybe
    {
        T Value { get; }
        T? ValueOrDefault { get; }
    }

    /// <summary>
    ///     A non-generic maybe useful for code that has to deal with maybes that aren't
    ///     generic.
    /// </summary>
    public interface IMaybe
    {
        bool HasValue { get; }
        object ObjectValue { get; }
        object? ObjectValueOrDefault { get; }
    }
}