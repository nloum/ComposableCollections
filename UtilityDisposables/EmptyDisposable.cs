using System;

namespace UtilityDisposables
{
    /// <summary>
    /// A singleton IDisposable that does nothing. This can come in handy at times.
    /// </summary>
    public sealed class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Default = new EmptyDisposable();

        private EmptyDisposable()
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}