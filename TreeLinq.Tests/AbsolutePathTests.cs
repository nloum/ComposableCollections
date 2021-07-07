using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeLinq.Tests
{
    [TestClass]
    public class AbsolutePathTests
    {
        [TestMethod]
        public void CombinationTests()
        {
            Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>> func = absPath => new[]
            {
                RelativeTreePath<string>.Empty / "c1" / "c2",
                RelativeTreePath<string>.Empty / "d1" / "d2"
            };
            
            var paths = new[]
            {
                RelativeTreePath<string>.Empty / "a1" / "a2",
                RelativeTreePath<string>.Empty / "b1" / "b2"
            };
            
            var uut = AbsoluteTreePath<string>.Root / "blahblah" / "blob" / paths / func;
            
            var absolutePaths = uut.ToImmutableList();
            absolutePaths.Count.Should().Be(4);
            absolutePaths[0].Should<AbsoluteTreePath<string>>().Be(new AbsoluteTreePath<string>("blahblah", "blob", "a1", "a2", "c1", "c2"));
            absolutePaths[1].Should<AbsoluteTreePath<string>>().Be(new AbsoluteTreePath<string>("blahblah", "blob", "a1", "a2", "d1", "d2"));
            absolutePaths[2].Should<AbsoluteTreePath<string>>().Be(new AbsoluteTreePath<string>("blahblah", "blob", "b1", "b2", "c1", "c2"));
            absolutePaths[3].Should<AbsoluteTreePath<string>>().Be(new AbsoluteTreePath<string>("blahblah", "blob", "b1", "b2", "d1", "d2"));
        }
    }
}