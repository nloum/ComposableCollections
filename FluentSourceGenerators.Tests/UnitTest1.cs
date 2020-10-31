using FluentAssertions;
using FluentSourceGenerators.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSourceGenerators.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public interface ITestBaseInterface
        {
            int BaseMethod();
        }
        
        [AnonymousImplementation(new[]{typeof(ITestBaseInterface)})]
        public interface ITestInterface : ITestBaseInterface
        {
            int GetExtraValue();
        }

        public class TestBase : ITestBaseInterface
        {
            public int BaseMethod()
            {
                return 34;
            }
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            ITestInterface uut = new AnonymousTestInterface(() => 35);
            uut.GetExtraValue().Should().Be(35);
            uut.BaseMethod().Should().Be(34);
        }
    }
}
