using System.Collections.Generic;
using System.Linq;
using ComposableCollections.List;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests.List
{
    [TestClass]
    public class AggregateOrderedListTests
    {
        [TestMethod]
        public void ShouldEnumerateInOrder()
        {
            var list1 = new OrderedList<int>();
            var list2 = new OrderedList<int>();
            list1.Add(1);
            list2.Add(2);
            list1.Add(3);
            list2.Add(4);
            
            var uut = new AggregateOrderedList<int>();
            uut.Add(list2);
            uut.Add(list1);

            uut.ToList().Should().BeInAscendingOrder();
        }

        [TestMethod]
        public void CompositionShouldWork()
        {
            var list1 = new List<int>()
            {
                5, 3, 6, 4, 9, 5
            };
            var list2 = new List<int>()
            {
                2, 6, 3, 8, 1, 9
            };

            var list1Ordered = list1.OrderBy(x => x);
            var list2Ordered = list2.OrderBy(x => x);

            list1Ordered.Count.Should().Be(6);
            list1Ordered.Should().BeInAscendingOrder();
            list2Ordered.Count.Should().Be(6);
            list2Ordered.Should().BeInAscendingOrder();

            var result = list1Ordered.Concat(list2Ordered);
            
            result.Should().BeInAscendingOrder();
            result.Count.Should().Be(12);
        }
    }
}