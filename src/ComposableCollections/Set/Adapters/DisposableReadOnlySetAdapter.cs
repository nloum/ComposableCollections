using System;
using ComposableCollections.Set.Base;

namespace ComposableCollections.Set.Adapters
{
    public class DisposableReadOnlySetAdapter<TValue> : DelegateReadOnlySet<TValue>, IDisposableReadOnlySet<TValue>
    {
        private readonly IDisposable _disposable;

        public DisposableReadOnlySetAdapter(IReadOnlySet<TValue> state, IDisposable disposable) : base(state)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}