using System.Collections.Generic;
using System.Linq;
using ComposableCollections;
using ComposableCollections.Dictionary;
using ComposableCollections.List;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoreCollections.Tests
{
    [TestClass]
    public class TakeReadOnlyListTests
    {
        [TestMethod]
        public void TakeRangeShouldWork()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            source.Add("d");
            source.Add("e");

            var uut = source.Take(GenericNumbers.NumbersUtility.Range(1, 3));
            uut.Count.Should().Be(2);
            uut[0].Should().Be("b");
            uut[1].Should().Be("c");
        }
        
        [TestMethod]
        public void ShouldLimitItems()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            
            var uut = new TakeReadOnlyList<string>(source, 2);
            uut.Count.Should().Be(2);
            uut[0].Should().Be("a");
            uut[1].Should().Be("b");
        }
        
        [TestMethod]
        public void ShouldTakeZeroItemsIfSpecified()
        {
            var source = new List<string>();
            source.Add("a");
            source.Add("b");
            source.Add("c");
            
            var uut = new TakeReadOnlyList<string>(source, 0);
            uut.Count.Should().Be(0);
        }
        
        [TestMethod]
        public void ShouldReturnCorrectNumberOfItemsIfMoreItemsAreTakenThanAvailable()
        {
            var source = new List<string>();
            
            source.Add("a");
            source.Add("b");
            source.Add("c");

            var uut = new TakeReadOnlyList<string>(source, 4);
            uut.Count.Should().Be(3);
            uut[0].Should().Be("a");
            uut[1].Should().Be("b");
            uut[2].Should().Be("c");
        }
    }
}