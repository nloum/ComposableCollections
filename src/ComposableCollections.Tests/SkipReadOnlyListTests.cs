using System.Collections.Generic;
using System.Linq;
using ComposableCollections.List;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoreCollections.Tests
{
    [TestClass]
    public class SkipReadOnlyListTests
    {
        [TestMethod]
        public void ShouldSkipItems()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            
            var uut = new SkipReadOnlyList<string>(source, 1);
            uut.Count.Should().Be(2);
            uut[0].Should().Be("b");
            uut[1].Should().Be("c");
        }
        
        [TestMethod]
        public void ShouldSkipZeroItemsIfSpecified()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            
            var uut = new SkipReadOnlyList<string>(source, 0);
            uut.Count.Should().Be(3);
            uut[0].Should().Be("a");
            uut[1].Should().Be("b");
            uut[2].Should().Be("c");
        }
        
        [TestMethod]
        public void ShouldSkipAllItemsIfSpecified()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            
            var uut = new SkipReadOnlyList<string>(source, 3);
            uut.Count.Should().Be(0);
        }
    }
}