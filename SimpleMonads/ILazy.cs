using System.ComponentModel;

namespace SimpleMonads
{
    public interface ILazy<out T> : IMonad<T>, ILazy, INotifyPropertyChanged
    {
        T Value { get; }
    }

    public interface ILazy
    {
        LazyState LazyState { get; }
        object ObjectValue { get; }
    }
}