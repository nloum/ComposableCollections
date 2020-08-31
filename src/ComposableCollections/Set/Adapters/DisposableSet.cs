using System;
using ComposableCollections.Set.Base;

namespace ComposableCollections.Set.Adapters
{
    public class DisposableSet<TValue> : DelegateSet<TValue>, IDisposableReadOnlySet<TValue>
    {
        private readonly IDisposable _disposable;

        public DisposableSet(IComposableSet<TValue> state, IDisposable disposable) : base(state)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}