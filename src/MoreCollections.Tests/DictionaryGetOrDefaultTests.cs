using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMonads;

namespace MoreCollections.Tests
{
    [TestClass]
    public class DictionaryGetOrDefaultTests
    {
        [TestMethod]
        public void NonExistentItemShouldBeCreatedAndPersistedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out IMaybe<Guid> value, out bool persist) =>
            {
                value = Guid.NewGuid().ToMaybe();
                persist = true;
            });

            var firstAccess = uut["Hi"];
            var secondAccess = uut["Hi"];

            firstAccess.Should().Be(secondAccess);
        }
        
        [TestMethod]
        public void NonExistentItemShouldBeCreatedAndNotPersistedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out IMaybe<Guid> value, out bool persist) =>
            {
                value = Guid.NewGuid().ToMaybe();
                persist = false;
            });

            var firstAccess = uut["Hi"];
            var secondAccess = uut["Hi"];

            firstAccess.Should().NotBe(secondAccess);
        }
        
        [TestMethod]
        public void NonExistentItemShouldNotBeCreatedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out IMaybe<Guid> value, out bool persist) =>
            {
                value = Maybe<Guid>.Nothing();
                persist = false;
            });

            Action action = () => Console.WriteLine(uut["Hi"]);

            action.Should().ThrowExactly<KeyNotFoundException>();
        }
    }
}