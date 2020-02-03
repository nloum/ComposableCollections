﻿using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilityDisposables.Tests
{
    [TestClass]
    public class DisposableCollectorTests
    {
        [TestMethod]
        public void ShouldDisposeMultipleDisposables()
        {
            var disposable1 = new TestDisposable();
            var disposable2 = new TestDisposable();
            var uut = new DisposableCollector(disposable1);
            uut.Disposes(disposable1);
            uut.Dispose();
            disposable1.IsDisposed.Should().BeTrue();
            disposable2.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldFailIfTheSameDisposableIsAddedMultipleTimes()
        {
            var disposable1 = new TestDisposable();
            var uut = new DisposableCollector(disposable1);
            Action action = () => uut.Disposes(disposable1);
            action.Should().Throw<Exception>();
        }
    }
}
