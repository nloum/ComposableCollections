using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMonads.Tests
{
    [TestClass]
    public class EitherTests
    {
        [TestMethod]
        public void ToStringShouldReturnValidResult()
        {
            var uut = new Either<string, int>("Hi there");
            uut.ToString().Should().Be("Either<string, int>(string Item1: Hi there)");
        }
    }
}