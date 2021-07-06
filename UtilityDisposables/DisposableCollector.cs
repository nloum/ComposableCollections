using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UtilityDisposables
{
    /// <summary>
    ///     A class for easily collecting IDisposables together to be disposed all at once at a later time.
    ///     This class is thread-safe.
    /// </summary>
    public class DisposableCollector : ManagedDisposable, IDisposableCollector
    {
        private readonly object _lock = new object();

        private HashSet<IDisposable> _disposables =
            new HashSet<IDisposable>(new ObjectReferenceEqualityComparer<IDisposable>());

        public DisposableCollector(IEnumerable<IDisposable> disposables)
        {
            Disposes(disposables);
        }

        public DisposableCollector(params IDisposable[] disposables)
        {
            Disposes(disposables);
        }

        public bool IsDisposed => _disposables == null;

        public void Disposes(IEnumerable<IDisposable> disposables)
        {
            lock (_lock)
            {
                if (IsDisposed)
                    throw new InvalidOperationException("Cannot modify an already-disposed IDisposable");
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        public void Disposes(params IDisposable[] disposables)
        {
            lock (_lock)
            {
                if (IsDisposed)
                    throw new InvalidOperationException("Cannot modify an already-disposed IDisposable");
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        public T Disposes<T>(T disposable)
            where T : IDisposable
        {
            lock (_lock)
            {
                InternalAddDisposable(disposable);
                return disposable;
            }
        }

        public void Disposes<T>(params T[] disposables)
            where T : IDisposable
        {
            lock (_lock)
            {
                if (IsDisposed)
                    throw new InvalidOperationException("Cannot modify an already-disposed IDisposable");
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        private void InternalAddDisposable(IDisposable disposable)
        {
            if (IsDisposed) throw new InvalidOperationException("Cannot modify an already-disposed IDisposable");

            if (_disposables.Contains(disposable))
                throw new InvalidOperationException("Cannot add a disposable to the disposable collector twice");

            _disposables.Add(disposable);
        }

        public void TryDisposes(IEnumerable<IDisposable> disposables)
        {
            lock (_lock)
            {
                if (IsDisposed) return;
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        public void TryDisposes(params IDisposable[] disposables)
        {
            lock (_lock)
            {
                if (IsDisposed) return;
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        public T TryDisposes<T>(T disposable)
            where T : IDisposable
        {
            lock (_lock)
            {
                if (IsDisposed) return disposable;
                InternalAddDisposable(disposable);
                return disposable;
            }
        }

        public void TryDisposes<T>(params T[] disposables)
            where T : IDisposable
        {
            lock (_lock)
            {
                if (IsDisposed) return;
                foreach (var disposable in disposables) InternalAddDisposable(disposable);
            }
        }

        protected override void DisposeManagedResources()
        {
            lock (_lock)
            {
                if (IsDisposed) return;
                foreach (var disposable in _disposables) disposable.Dispose();
                _disposables = null;
            }
        }

        /// <summary>
        ///     A generic object comparerer that would only use object's reference,
        ///     ignoring any <see cref="IEquatable{T}" /> or <see cref="object.Equals(object)" />  overrides.
        /// </summary>
        /// <remarks>
        ///     https://stackoverflow.com/a/1890230
        /// </remarks>
        public class ObjectReferenceEqualityComparer<T> : EqualityComparer<T>
            where T : class
        {
            private static IEqualityComparer<T> _defaultComparer;

            public new static IEqualityComparer<T> Default =>
                _defaultComparer ?? (_defaultComparer = new ObjectReferenceEqualityComparer<T>());

            #region IEqualityComparer<T> Members

            public override bool Equals(T x, T y)
            {
                return ReferenceEquals(x, y);
            }

            public override int GetHashCode(T obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }

            #endregion
        }
    }
}