using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityDisposables
{
    public class ProtectedDisposableCollector : ManagedDisposable
    {
        private readonly DisposableCollector _disposables = new DisposableCollector();

        protected void Disposes(IEnumerable<IDisposable> disposables)
        {
            _disposables.Disposes(disposables);
        }

        protected void Disposes(params IDisposable[] disposables)
        {
            _disposables.Disposes(disposables);
        }

        protected T Disposes<T>(T disposable) where T : IDisposable
        {
            return _disposables.Disposes(disposable);
        }

        protected void Disposes<T>(params T[] disposables) where T : IDisposable
        {
            _disposables.Disposes(disposables);
        }

        protected void TryDisposes(IEnumerable<IDisposable> disposables)
        {
            _disposables.TryDisposes(disposables);
        }

        protected void TryDisposes(params IDisposable[] disposables)
        {
            _disposables.TryDisposes(disposables);
        }

        protected T TryDisposes<T>(T disposable) where T : IDisposable
        {
            return _disposables.TryDisposes(disposable);
        }

        protected void TryDisposes<T>(params T[] disposables) where T : IDisposable
        {
            _disposables.TryDisposes(disposables);
        }

        protected override void DisposeManagedResources()
        {
            _disposables.Dispose();
        }
    }
}
