using System;

namespace ComposableCollections.Utilities
{
    public static class Disposable
    {
        public static Disposable<T> Create<T>(T item, IDisposable disposable)
        {
            return new Disposable<T>(item, disposable);
        }
    }

    public class Disposable<T> : IDisposable
    {
        private readonly IDisposable _disposable;
        
        public T Value { get; }

        public Disposable(T value, IDisposable disposable)
        {
            Value = value;
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}