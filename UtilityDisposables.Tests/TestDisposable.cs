using System;

namespace UtilityDisposables.Tests
{
    public class TestDisposable : IDisposable
    {
        public bool IsDisposed { get; private set; } = false;

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
