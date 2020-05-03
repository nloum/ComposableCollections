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
            Func<RelativePath<string>, IEnumerable<RelativePath<string>>> func = absPath => new[]
            {
                RelativePath<string>.Empty / "c1" / "c2",
                RelativePath<string>.Empty / "d1" / "d2"
            };
            
            var paths = new[]
            {
                RelativePath<string>.Empty / "a1" / "a2",
                RelativePath<string>.Empty / "b1" / "b2"
            };
            
            var uut = RelativePath<string>.Empty / "blahblah" / "blob" / paths / func;
            
            var relativePaths = uut.ToImmutableList();
            relativePaths.Count.Should().Be(4);
            relativePaths[0].Should<RelativePath<string>>().Be(new RelativePath<string>("blahblah", "blob", "a1", "a2", "c1", "c2"));
            relativePaths[1].Should<RelativePath<string>>().Be(new RelativePath<string>("blahblah", "blob", "a1", "a2", "d1", "d2"));
            relativePaths[2].Should<RelativePath<string>>().Be(new RelativePath<string>("blahblah", "blob", "b1", "b2", "c1", "c2"));
            relativePaths[3].Should<RelativePath<string>>().Be(new RelativePath<string>("blahblah", "blob", "b1", "b2", "d1", "d2"));
        }
    }
}