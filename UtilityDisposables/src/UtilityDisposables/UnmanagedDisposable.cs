using System;

namespace UtilityDisposables
{
    /// <summary>
    /// A handy class to inherit when you have unmanaged resources to dispose of.
    /// These are resources that should be disposed of if the deconstructor of your object
    /// is called.
    /// </summary>
    public abstract class UnmanagedDisposable : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(true);
        }

        #endregion

        ~UnmanagedDisposable()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            DisposeUnmanagedResources();

            if (isDisposing)
            {
                DisposeManagedResources();
            }
        }

        protected abstract void DisposeManagedResources();

        /// <summary>
        /// Dispose of unmanaged resources here.
        /// </summary>
        protected abstract void DisposeUnmanagedResources();
    }
}
