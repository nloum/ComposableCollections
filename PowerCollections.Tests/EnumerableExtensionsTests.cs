using System;
using System.Linq;
using FluentAssertions;
using System.Collections.Generic;
using PowerCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Collections.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region Miscellaneous tests
        
        #region FirstOr

        [TestMethod]
        public void FirstOr_ReturnsFirstItem()
        {
            var items = new[] { 1, 2, 3, 4 };
            items.FirstOr(-1).Should().Be(1);
        }

        [TestMethod]
        public void FirstOr_ReturnsFirstMatchingItem()
        {
            var items = new[] { 1, 2, 3, 4 };
            items.FirstOr(i => i > 2, -1).Should().Be(3);
        }

        [TestMethod]
        public void FirstOr_ReturnsOtherwiseBecauseNoMatchingItem()
        {
            var items = new[] { 1, 2, 3, 4 };
            items.FirstOr(i => i > 10, -1).Should().Be(-1);
        }

        [TestMethod]
        public void FirstOr_ReturnsOtherwiseBecauseNoItems()
        {
            var items = new int[] { };
            items.FirstOr(i => i > 10, -1).Should().Be(-1);
        }

        #endregion

        #region GetAbsoluteIndex

        [TestMethod]
        public void GetAbsoluteIndex_WayTooNegativeIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(-10).Should().Be(2);
        }

        [TestMethod]
        public void GetAbsoluteIndex_SlightlyTooNegativeIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(-7).Should().Be(5);
        }

        [TestMethod]
        public void GetAbsoluteIndex_BarelyNotTooNegativeIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(-6).Should().Be(0);
        }

        [TestMethod]
        public void GetAbsoluteIndex_NotTooNegativeIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(-3).Should().Be(3);
        }

        [TestMethod]
        public void GetAbsoluteIndex_LastIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(5).Should().Be(5);
        }

        [TestMethod]
        public void GetAbsoluteIndex_MidAbsoluteIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(3).Should().Be(3);
        }

        [TestMethod]
        public void GetAbsoluteIndex_BarelyTooPositiveIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(6).Should().Be(0);
        }

        [TestMethod]
        public void GetAbsoluteIndex_WayTooPositiveIndex()
        {
            var list = new[] { 0, 1, 2, 3, 4, 5 };
            list.GetAbsoluteIndex(8).Should().Be(2);
        }

        #endregion

        [TestMethod]
        public void Between()
        {
            var data = new[] {0, 1, 2, 3, 1, 2, 5, 6, 2, 1, 2, 8, 9};
            var needle = new[] { 1, 2 };
            var matches = data.Searches(needle).ToList();
            matches.Should().BeEquivalentTo(1, 4, 9);
            var split = matches.Between(data).ToList();
            split.Count().Should().Be(4);
            split[0].Should().BeEquivalentTo(0);
            split[1].Should().BeEquivalentTo(2, 3);
            split[2].Should().BeEquivalentTo(2, 5, 6, 2);
            split[3].Should().BeEquivalentTo(2, 8, 9);
        }

        [TestMethod]
        public void Between_Ranges()
        {
            var data = new[] { 0, 1, 2, 3, 1, 2, 5, 6, 2, 1, 2, 8, 9 };
            var needle = new[] {1, 2};
            var matches = data.Searches(needle).Select(i => Tuple.Create(i, i + needle.Length - 1)).ToList();
            matches.Select(t => t.Item1).Should().BeEquivalentTo(1, 4, 9);
            var split = matches.Between(data).ToList();
            split.Count().Should().Be(4);
            split[0].Should().BeEquivalentTo(0);
            split[1].Should().BeEquivalentTo(3);
            split[2].Should().BeEquivalentTo(5, 6, 2);
            split[3].Should().BeEquivalentTo(8, 9);
        }

        [TestMethod]
        public void SplitWhere_IncludeSplitter()
        {
            var data = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var split = data.SplitWhere(i => i % 3 == 1, true).ToList();
            split[0].Should().BeEquivalentTo(0);
            split[1].Should().BeEquivalentTo(1, 2, 3);
            split[2].Should().BeEquivalentTo(4, 5, 6);
            split[3].Should().BeEquivalentTo(7, 8, 9);
        }

        [TestMethod]
        public void SplitWhere_EliminateSplitter()
        {
            var data = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var split = data.SplitWhere(i => i % 3 == 1, false).ToList();
            split[0].Should().BeEquivalentTo(0);
            split[1].Should().BeEquivalentTo(2, 3);
            split[2].Should().BeEquivalentTo(5, 6);
            split[3].Should().BeEquivalentTo(8, 9);
        }

        [TestMethod]
        public void BinarySearch_ForLastItem()
        {
            var haystack = new[] {1, 5, 8, 9, 10}.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(10));
            result.Should().Be(4);
        }

        [TestMethod]
        public void BinarySearch_ForNonExistentItem()
        {
            var haystack = new[] { 1, 5, 8, 9, 10 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(7));
            result.Should().Be(-1);
        }

        [TestMethod]
        public void BinarySearch_InEmptyArray()
        {
            var haystack = new int[0].OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(10));
            result.Should().Be(-1);
        }

        [TestMethod]
        public void BinarySearch_ForFirstItem()
        {
            var haystack = new[] { 1, 5, 8, 9, 10 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(1));
            result.Should().Be(0);
        }

        [TestMethod]
        public void BinarySearch_ForItemInMiddle()
        {
            var haystack = new[] { 1, 5, 8, 9, 10 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(8));
            result.Should().Be(2);
        }

        [TestMethod]
        public void BinarySubsetSearch_ReversedMinMax()
        {
            var haystack = new[] { 1, 5, 8, 9, 10 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(1), -1, 0);
            result.Should().Be(0);
        }

        [TestMethod]
        public void BinarySubsetSearch_ForLastItem()
        {
            var haystack = new[] { -3, -4, 1, 5, 8, 9, 10, 11, 14 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(10), 2, -3);
            result.Should().Be(4 + 2);
        }

        [TestMethod]
        public void BinarySubsetSearch_ForNonExistentItem()
        {
            var haystack = new[] { -3, -4, 1, 5, 8, 9, 10, 11, 14 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(7), 2, -3);
            result.Should().Be(-1);
        }

        [TestMethod]
        public void BinarySubsetSearch_InEmptyArray()
        {
            var haystack = new int[0].OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(10), 2, -3);
            result.Should().Be(-1);
        }

        [TestMethod]
        public void BinarySubsetSearch_ForFirstItem()
        {
            var haystack = new[] { -3, -4, 1, 5, 8, 9, 10, 11, 14 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(1), 2, -3);
            result.Should().Be(0 + 2);
        }

        [TestMethod]
        public void BinarySubsetSearch_ForItemInMiddle()
        {
            var haystack = new[] { -3, -4, 1, 5, 8, 9, 10, 11, 14 }.OrderBy(i => i).ToList();
            var result = haystack.BinarySearch(i => i.CompareTo(8), 2, -3);
            result.Should().Be(2 + 2);
        }

        #endregion

        #region RemoveWhere tests

        [TestMethod]
        public void RemoveWhere_ShouldGoThroughAllElements()
        {
            var array = new[] { 1, 2, 3, 4, 5 };
            var count = 0;
            array.RemoveWhere(i =>
            {
                count++;
                return false;
            });
            array.Should().BeEquivalentTo(1, 2, 3, 4, 5);
        }

        [TestMethod]
        public void RemoveWhere_RemoveElementsProperly()
        {
            var array = new[] { 1, 2, 3, 4, 5 }.ToList();
            array.RemoveWhere(i => i % 2 == 0);
            array.Should().BeEquivalentTo(1, 3, 5);
        }

        [TestMethod]
        public void RemoveBackwardsWhere_ShouldGoThroughAllElements()
        {
            var array = new[] { 1, 2, 3, 4, 5 };
            var count = 0;
            array.RemoveBackwardsWhere((i, item) =>
            {
                count++;
                return false;
            });
            array.Should().BeEquivalentTo(1, 2, 3, 4, 5);
        }

        [TestMethod]
        public void RemoveBackwardsWhere_RemoveElementsProperly()
        {
            var array = new[] { 1, 2, 3, 4, 5 }.ToList();
            array.RemoveBackwardsWhere((i, item) => item % 2 == 0);
            array.Should().BeEquivalentTo(1, 3, 5);
        }

        #endregion

        #region Sort tests

        [TestMethod]
        public void SortedInsert_EvenCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 5, 6, 7 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 4, 5, 6, 7);
        }

        [TestMethod]
        public void SortedInsert_WithDuplicatesBefore_EvenCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 3, 5, 6 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 3, 4, 5, 6);
        }

        [TestMethod]
        public void SortedInsert_WithDuplicatesAfter_EvenCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 5, 5, 6 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 4, 5, 5, 6);
        }

        [TestMethod]
        public void SortedInsert_AtEnd_EvenCount()
        {
            var sortedList = new List<int>() { 0, 1, 2, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(0, 1, 2, 3, 4);
        }

        [TestMethod]
        public void SortedInsert_AtBeginning_EvenCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 4 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(0, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(0, 1, 2, 3, 4);
        }

        [TestMethod]
        public void SortedInsert_AtEnd_WithDuplicates_EvenCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 3, 4);
        }

        [TestMethod]
        public void SortedInsert_AtBeginning_WithDuplicates_EvenCount()
        {
            var sortedList = new List<int>() { 1, 1, 2, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(0, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(0, 1, 1, 2, 3);
        }
        
        [TestMethod]
        public void SortedInsert_OddCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 5, 6, 7, 8 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 4, 5, 6, 7, 8);
        }

        [TestMethod]
        public void SortedInsert_WithDuplicatesBefore_OddCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 3, 5, 6, 7 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 3, 4, 5, 6, 7);
        }

        [TestMethod]
        public void SortedInsert_WithDuplicatesAfter_OddCount()
        {
            var sortedList = new List<int>() { 1, 2, 3, 5, 5, 6, 7 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 4, 5, 5, 6, 7);
        }

        [TestMethod]
        public void SortedInsert_AtEnd_OddCount()
        {
            var sortedList = new List<int>() { 1, 2, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(1, 2, 3, 4);
        }

        [TestMethod]
        public void SortedInsert_AtBeginning_OddCount()
        {
            var sortedList = new List<int>() { 1, 2, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(0, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(0, 1, 2, 3);
        }

        [TestMethod]
        public void SortedInsert_AtEnd_WithDuplicates_OddCount()
        {
            var sortedList = new List<int>() { 2, 3, 3 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(4, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(2, 3, 3, 4);
        }

        [TestMethod]
        public void SortedInsert_AtBeginning_WithDuplicates_OddCount()
        {
            var sortedList = new List<int>() { 1, 1, 2 }.OrderBy(i => i).ToList();
            sortedList.SortedInsert(0, (a, b) => a.CompareTo(b));
            sortedList.Should().ContainInOrder(0, 1, 1, 2);
        }

        #endregion

        #region Search-related tests

        [TestMethod]
        public void Search_ShouldFailForNonexistentStrings()
        {
            new[] {0, 2, 3, 0}.ToList().Search(new[] {3, 2}).Should().Be(-1);
        }

        [TestMethod]
        public void Search_ReverseStartAndStop_ShouldWork()
        {
            new[] { 0, 2, 3, 0 }.ToList().Search(new[] { 2, 3 }, -1, 0).Should().Be(1);
        }

        [TestMethod]
        public void Searches()
        {
            new[] { 0, 2, 3, 0, 2, 6, 7, 0, 2 }.Searches(new[] { 0, 2 }).ToList().Should().BeEquivalentTo(0, 3, 7);
        }

        [TestMethod]
        public void Search_PositiveStart_PositiveStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, 1, 6)
                .Should().Be(2);
        }

        [TestMethod]
        public void Search_PositiveStart_PositiveStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, 1, 6)
                .Should().Be(3);
        }

        [TestMethod]
        public void Search_NegativeStart_PositiveStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, -7, 6)
                .Should().Be(2);
        }

        [TestMethod]
        public void Search_NegativeStart_PositiveStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, -7, 6)
                .Should().Be(3);
        }
        
        [TestMethod]
        public void Search_PositiveStart_NegativeStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, 1, -2)
                .Should().Be(2);
        }

        [TestMethod]
        public void Search_PositiveStart_NegativeStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, 1, -2)
                .Should().Be(3);
        }

        [TestMethod]
        public void Search_NegativeStart_NegativeStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, -7, -2)
                .Should().Be(2);
        }
        
        [TestMethod]
        public void Search_NegativeStart_NegativeStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().Search(new[] { 2, 3 }, -7, -2)
                .Should().Be(3);
        }
        
        [TestMethod]
        public void ReverseSearch_ShouldFailForNonexistentStrings()
        {
            new[] { 0, 2, 3, 0 }.ToList().ReverseSearch(new[] { 3, 2 }).Should().Be(-1);
        }

        [TestMethod]
        public void ReverseSearch_ReverseStartAndStop_ShouldWork()
        {
            new[] { 0, 2, 3, 0 }.ToList().ReverseSearch(new[] { 2, 3 }, -1, 0).Should().Be(1);
        }

        [TestMethod]
        public void ReverseSearches()
        {
            new[] { 0, 2, 3, 0, 2, 6, 7, 0, 2 }.ReverseSearches(new[] { 0, 2 }).ToList().Should().BeEquivalentTo(7, 3, 0);
        }

        [TestMethod]
        public void ReverseSearch_PositiveStart_PositiveStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, 1, 6)
                .Should().Be(4);
        }

        [TestMethod]
        public void ReverseSearch_PositiveStart_PositiveStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, 1, 6)
                .Should().Be(5);
        }

        [TestMethod]
        public void ReverseSearch_NegativeStart_PositiveStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, -7, 6)
                .Should().Be(4);
        }

        [TestMethod]
        public void ReverseSearch_NegativeStart_PositiveStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, -7, 6)
                .Should().Be(5);
        }

        [TestMethod]
        public void ReverseSearch_PositiveStart_NegativeStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, 1, -2)
                .Should().Be(4);
        }

        [TestMethod]
        public void ReverseSearch_PositiveStart_NegativeStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, 1, -2)
                .Should().Be(5);
        }

        [TestMethod]
        public void ReverseSearch_NegativeStart_NegativeStop_1()
        {
            new[] { 2, 3, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, -7, -2)
                .Should().Be(4);
        }

        [TestMethod]
        public void ReverseSearch_NegativeStart_NegativeStop_2()
        {
            new[] { 2, 3, 0, 2, 3, 2, 3, 2, 3 }.ToList().ReverseSearch(new[] { 2, 3 }, -7, -2)
                .Should().Be(5);
        }

        [TestMethod]
        public void LongestCommonOrderedSet()
        {
            new[] {1, 2, 3, 4}.LongestCommonOrderedSubset(new[] {7, 8, 2, 3, 4, 5, 6})
                .Should().BeEquivalentTo(2, 3, 4);
        }

        #endregion

        #region Subset tests

        [TestMethod]
        public void ArraySubset_PositiveStart()
        {
            new[] {1, 2, 3, 4}.Subset(2)
                .Should()
                .BeEquivalentTo(3, 4);
        }

        [TestMethod]
        public void ArraySubset_PositiveStartPositiveStop()
        {
            new[] {1, 2, 3, 4}.Subset(1, 3)
                .Should()
                .BeEquivalentTo(2, 3, 4);
        }

        [TestMethod]
        public void Subset_PositiveStart()
        {
            new[] {1, 2, 3, 4}.AsEnumerable().Subset(2)
                .Should()
                .BeEquivalentTo(3, 4);
        }

        [TestMethod]
        public void Subset_PositiveStartPositiveStop()
        {
            new[] {1, 2, 3, 4}.AsEnumerable().Subset(1, 3)
                .Should()
                .BeEquivalentTo(2, 3, 4);
        }

        [TestMethod]
        public void ArraySubset_NegativeStart()
        {
            new[] { 1, 2, 3, 4 }.Subset(-2)
                .Should()
                .BeEquivalentTo(3, 4);
        }

        [TestMethod]
        public void ArraySubset_NegativeStartNegativeStop()
        {
            new[] { 1, 2, 3, 4 }.Subset(-3, -2)
                .Should()
                .BeEquivalentTo(2, 3);
        }

        [TestMethod]
        public void ArraySubset_NegativeStartPositiveStop()
        {
            new[] { 1, 2, 3, 4 }.Subset(-3, 3)
                .Should()
                .BeEquivalentTo(2, 3, 4);
        }

        [TestMethod]
        public void ArraySubset_PositiveStartNegativeStop()
        {
            new[] { 1, 2, 3, 4 }.Subset(1, -2)
                .Should()
                .BeEquivalentTo(2, 3);
        }

        [TestMethod]
        public void Subset_NegativeStart()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(2)
                .Should()
                .BeEquivalentTo(3, 4);
        }

        [TestMethod]
        public void Subset_NegativeStartNegativeStop()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(-3, -2)
                .Should()
                .BeEquivalentTo(2, 3);
        }

        [TestMethod]
        public void Subset_NegativeStartPositiveStop()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(-3, 3)
                .Should()
                .BeEquivalentTo(2, 3, 4);
        }

        [TestMethod]
        public void Subset_PositiveStartNegativeStop()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(1, -2)
                .Should()
                .BeEquivalentTo(2, 3);
        }

        [TestMethod]
        public void Subset_FirstElement()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(0, 0)
                .Should()
                .BeEquivalentTo(1);
        }

        [TestMethod]
        public void Subset_LastElement()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(-1, -1)
                .Should()
                .BeEquivalentTo(4);
        }

        [TestMethod]
        public void Subset_MiddleElement()
        {
            new[] { 1, 2, 3, 4 }.AsEnumerable().Subset(2, 2)
                .Should()
                .BeEquivalentTo(3);
        }

        #endregion
    }
}
