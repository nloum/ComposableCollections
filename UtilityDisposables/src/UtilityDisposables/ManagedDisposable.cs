using System;

namespace UtilityDisposables
{
    /// <summary>
    /// A class that implements the recommended IDisposable pattern.
    /// See: http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P
    /// </summary>
    public abstract class ManagedDisposable : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(true);
        }

        #endregion

        /// <summary>
        /// Don't override this unless you know what you are doing. Call this with a parameter of false
        /// from your deconstructor.
        /// </summary>
        /// <param name="isDisposing">True if we are called from the Dispose() method;
        /// false if we are called from a deconstructor.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                DisposeManagedResources();
            }
        }

        /// <summary>
        /// Free managed objects here.
        /// </summary>
        protected abstract void DisposeManagedResources();

        ~ManagedDisposable()
        {
            Dispose(false);
        }
    }
}
