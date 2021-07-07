using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeLinq.Tests
{
    [TestClass]
    public class RelativePathTests
    {
        [TestMethod]
        public void CombinationTests()
        {
            Func<RelativeTreePath<string>, IEnumerable<RelativeTreePath<string>>> func = absPath => new[]
            {
                RelativeTreePath<string>.Empty / "c1" / "c2",
                RelativeTreePath<string>.Empty / "d1" / "d2"
            };
            
            var paths = new[]
            {
                RelativeTreePath<string>.Empty / "a1" / "a2",
                RelativeTreePath<string>.Empty / "b1" / "b2"
            };
            
            var uut = RelativeTreePath<string>.Empty / "blahblah" / "blob" / paths / func;
            
            var relativePaths = uut.ToImmutableList();
            relativePaths.Count.Should().Be(4);
            relativePaths[0].Should<RelativeTreePath<string>>().Be(new RelativeTreePath<string>("blahblah", "blob", "a1", "a2", "c1", "c2"));
            relativePaths[1].Should<RelativeTreePath<string>>().Be(new RelativeTreePath<string>("blahblah", "blob", "a1", "a2", "d1", "d2"));
            relativePaths[2].Should<RelativeTreePath<string>>().Be(new RelativeTreePath<string>("blahblah", "blob", "b1", "b2", "c1", "c2"));
            relativePaths[3].Should<RelativeTreePath<string>>().Be(new RelativeTreePath<string>("blahblah", "blob", "b1", "b2", "d1", "d2"));
        }
    }
}