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
            Func<AbsolutePath<string>, IEnumerable<RelativePath<string>>> func = absPath => new[]
            {
                RelativePath<string>.Empty / "c1" / "c2",
                RelativePath<string>.Empty / "d1" / "d2"
            };
            
            var paths = new[]
            {
                RelativePath<string>.Empty / "a1" / "a2",
                RelativePath<string>.Empty / "b1" / "b2"
            };
            
            var uut = AbsolutePath<string>.Root / "blahblah" / "blob" / paths / func;
            
            var absolutePaths = uut.ToImmutableList();
            absolutePaths.Count.Should().Be(4);
            absolutePaths[0].Should<AbsolutePath<string>>().Be(new AbsolutePath<string>("blahblah", "blob", "a1", "a2", "c1", "c2"));
            absolutePaths[1].Should<AbsolutePath<string>>().Be(new AbsolutePath<string>("blahblah", "blob", "a1", "a2", "d1", "d2"));
            absolutePaths[2].Should<AbsolutePath<string>>().Be(new AbsolutePath<string>("blahblah", "blob", "b1", "b2", "c1", "c2"));
            absolutePaths[3].Should<AbsolutePath<string>>().Be(new AbsolutePath<string>("blahblah", "blob", "b1", "b2", "d1", "d2"));
        }
    }
}