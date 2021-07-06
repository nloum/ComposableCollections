using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilityDisposables.Tests
{

    [TestClass]
    public class AnonymousDisposableTests
    {
        [TestMethod]
        public void ShouldDisposeMultipleParameters()
        {
            var didDispose1 = false;
            var didDispose2 = false;
            var uut = new AnonymousDisposable(() => didDispose1 = true, () => didDispose2 = true);
            uut.Dispose();
            didDispose1.Should().BeTrue();
            didDispose2.Should().BeTrue();
        }
    }
}
