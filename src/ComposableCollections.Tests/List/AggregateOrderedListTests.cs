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
    }
}