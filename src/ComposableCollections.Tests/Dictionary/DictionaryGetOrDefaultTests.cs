using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Sources;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMonads;

namespace ComposableCollections.Tests
{
    [TestClass]
    public class DictionaryGetOrDefaultTests
    {
        [TestMethod]
        public void AddShouldOnlyWorkIfTheItemDoesNotExistYet()
        {
            var uut = new ComposableDictionary<string, int>();
            uut["ha"] = 2;
            uut.ContainsKey("ha").Should().BeTrue();
            uut.TryGetValue("ha", out var _).Should().BeTrue();
            uut["ha"].Should().Be(2);
            uut.ContainsKey("hey").Should().BeFalse();
            uut.TryGetValue("hey", out var _).Should().BeFalse();
        }
        
        [TestMethod]
        public void ConcurrentAddShouldOnlyWorkIfTheItemDoesNotExistYet()
        {
            var uut = new ConcurrentDictionary<string, int>();
            uut["ha"] = 2;
            uut.ContainsKey("ha").Should().BeTrue();
            uut.TryGetValue("ha", out var _).Should().BeTrue();
            uut["ha"].Should().Be(2);
            uut.ContainsKey("hey").Should().BeFalse();
            uut.TryGetValue("hey", out var _).Should().BeFalse();
        }

        [TestMethod]
        public void NonExistentItemShouldBeCreatedAndPersistedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out Guid value, out bool persist) =>
            {
                value = Guid.NewGuid();
                persist = true;
                return true;
            });

            var firstAccess = uut["Hi"];
            var secondAccess = uut["Hi"];

            firstAccess.Should().Be(secondAccess);
        }
        
        [TestMethod]
        public void NonExistentItemShouldBeCreatedAndNotPersistedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out Guid value, out bool persist) =>
            {
                value = Guid.NewGuid();
                persist = false;
                return true;
            });

            var firstAccess = uut["Hi"];
            var secondAccess = uut["Hi"];

            firstAccess.Should().NotBe(secondAccess);
        }
        
        [TestMethod]
        public void NonExistentItemShouldNotBeCreatedIfSpecified()
        {
            var uut = new DictionaryGetOrDefault<string, Guid>((string key, out Guid value, out bool persist) =>
            {
                value = default;
                persist = false;
                return false;
            });

            Action action = () => Console.WriteLine(uut["Hi"]);

            action.Should().ThrowExactly<KeyNotFoundException>();
        }
    }
}