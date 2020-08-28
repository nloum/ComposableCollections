using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests.Adapters
{
    [TestClass]
    public class QueryableToQueryableReadOnlyDictionaryAdapterTests
    {
        [TestMethod]
        public void ShouldWorkWithInMemoryQueryable()
        {
            var source = new List<string>();
            source.Add("Hi");
            source.Add("Hello");
            var uut = source.AsQueryable().ToQueryableReadOnlyDictionary(x => x.Length);
            var keyValues = uut.ToList();
            keyValues.Count.Should().Be(2);
            keyValues[0].Key.Should().Be(2);
            keyValues[0].Value.Should().Be("Hi");
            keyValues[1].Key.Should().Be(5);
            keyValues[1].Value.Should().Be("Hello");

            uut.ContainsKey(2).Should().BeTrue();
            uut.ContainsKey(5).Should().BeTrue();
            uut.ContainsKey(3).Should().BeFalse();

            uut[2].Should().Be("Hi");
            uut[5].Should().Be("Hello");
        }
    }
}