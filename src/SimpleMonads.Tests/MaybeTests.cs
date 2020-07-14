using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static SimpleMonads.Utility;

namespace SimpleMonads.Tests
{
    [TestClass]
    public class MaybeTests
    {
        [TestMethod]
        public void ValueOfNothingShouldThrowSpecifiedException()
        {
            var uut = Nothing<string>(() => throw new DirectoryNotFoundException("Weird, isn't it?"));

            Action action = () =>
            {
                var x = uut.Value;
            };

            action.Should()
                .ThrowExactly<DirectoryNotFoundException>("this is the exception specified when creating the nothing");
        }
    }
}