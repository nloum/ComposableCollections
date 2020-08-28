using System;

namespace ComposableCollections.Utilities
{
    public class Disposable<T> : IDisposable
    {
        private readonly IDisposable _disposable;
        
        public T Value { get; }

        public Disposable(IDisposable disposable, T value)
        {
            _disposable = disposable;
            Value = value;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}