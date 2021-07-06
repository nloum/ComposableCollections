using System;
using System.Collections.Generic;

namespace UtilityDisposables
{
    /// <summary>
    /// A class for easily collecting IDisposables together to be disposed all at once at a later time.
    /// </summary>
    public interface IDisposableCollector : IDisposable
    {
        void Disposes(params IDisposable[] disposables);
        void Disposes(IEnumerable<IDisposable> disposables);
        T Disposes<T>(T disposable) where T : IDisposable;
        void Disposes<T>(params T[] disposables) where T : IDisposable;
    }
}
