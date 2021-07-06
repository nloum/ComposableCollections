using System;
using System.Collections.Generic;

namespace UtilityDisposables
{
    public static class Extensions
    {
        /// <summary>
        /// Combines multiple IDisposables into a single IDisposable
        /// </summary>
        public static IDisposable ToDisposable(this IEnumerable<IDisposable> disposables)
        {
            return new DisposableCollector(disposables);
        }

        /// <summary>
        /// Combines multiple IDisposables into a single IDisposable
        /// </summary>
        public static IDisposable DisposeWith(this IDisposable firstDisposable, params IDisposable[] rest)
        {
            var result = new DisposableCollector();
            result.Disposes(firstDisposable);
            result.Disposes(rest);
            return result;
        }

        /// <summary>
        /// Returns an IDisposable that, when disposed, first disposes firstDisposable, and then calls rest;
        /// if rest returns null, nothing more happens, but if rest returns a non-null IDisposable, then that
        /// also is disposed.
        /// </summary>
        public static IDisposable DisposeWith(this IDisposable firstDisposable, Func<IDisposable> rest)
        {
            return new AnonymousDisposable(() =>
            {
                firstDisposable.Dispose();
                var restResult = rest();
                restResult?.Dispose();
            });
        }
        
        /// <summary>
        /// Returns an IDisposable that, when disposed, first disposes firstDisposable and then calls rest.
        /// </summary>
        public static IDisposable DisposeWith(this IDisposable firstDisposable, Action rest)
        {
            return new AnonymousDisposable(() =>
            {
                firstDisposable.Dispose();
                rest();
            });
        }
    }
}
