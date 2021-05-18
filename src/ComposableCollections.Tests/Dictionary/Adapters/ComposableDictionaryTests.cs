using ComposableCollections.Dictionary.Sources;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests.Adapters
{
    [TestClass]
    public class ComposableDictionaryTests
    {
        [TestMethod]
        public void TryAddShouldNotOverride()
        {
            var uut = new ComposableDictionary<int, string>();
            uut.Add(2, "Hi");
            uut.TryAdd(2, () => "Hello", out var result, out var previousValue)
                .Should().BeFalse();
            result.Should().Be("Hi");
            previousValue.Should().Be("Hi");
            uut[2].Should().Be("Hi");
        }
        
        [TestMethod]
        public void TryAddShouldSetNewValue()
        {
            var uut = new ComposableDictionary<int, string>();
            uut.Add(2, "Hi");
            uut.TryAdd(3, () => "Hello", out var result, out var previousValue)
                .Should().BeTrue();
            result.Should().Be("Hello");
            previousValue.Should().BeNull();
            uut[2].Should().Be("Hi");
            uut[3].Should().Be("Hello");
        }
    }
}