using System;

namespace UtilityDisposables
{
    /// <summary>
    /// A decorator class that only calls Dispose on the inner <see cref="T"/> value once all the references are gone.
    /// </summary>
    public class ReferenceCounter<T> : IDisposable
        where T : class, IDisposable
    {
        private readonly Func<T> _func;
        private T _value;
        private ulong _referenceCount = 0;

        public ReferenceCounter(Func<T> func)
        {
            _func = func;
            if (_func == null)
                throw new NullReferenceException();
        }

        public T Value => _value;

        /// <summary>
        /// Get an object that will keep the inner <see cref="T"/> value from being Disposed
        /// until the return value of all calls to GetReference are Disposed.
        /// </summary>
        public ReferenceCounter<T> GetReference()
        {
            if (_value == null)
                _value = _func();
            _referenceCount++;
            return this;
        }

        public void Dispose()
        {
            if (_referenceCount <= 1)
            {
                if (_value != null)
                    _value.Dispose();
                _value = null;
            }
            _referenceCount--;
        }
    }

    /// <summary>
    /// Non-generic form of <see cref="ReferenceCounter{T}"/> that deals just in <see cref="IDisposable"/>s.
    /// </summary>
    public class ReferenceCounter : ReferenceCounter<IDisposable>
    {
        public ReferenceCounter(Func<IDisposable> func)
            : base(func)
        {
        }
    }
}
