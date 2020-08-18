using System;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests
{
    [TestClass]
    public class CommonCachingDictionaryTests
    {
        public enum CachingDictionaryType
        {
            ConcurrentCachingDictionaryWithMinimalState,
            ConcurrentCachingDictionary,
        }
        
        private ICacheDictionary<TKey, TValue> CreateUnitUnderTest<TKey, TValue>(CachingDictionaryType cachingDictionaryType, IComposableDictionary<TKey, TValue> flushCacheTo)
        {
            switch (cachingDictionaryType)
            {
                case CachingDictionaryType.ConcurrentCachingDictionaryWithMinimalState:
                    return new ConcurrentCachingDictionaryWithMinimalState<TKey, TValue>(flushCacheTo);
                case CachingDictionaryType.ConcurrentCachingDictionary:
                    return new ConcurrentCachingDictionary<TKey, TValue>(flushCacheTo, new ComposableDictionary<TKey, TValue>());
                default:
                    throw new NotImplementedException();
            }
        }

        [TestMethod]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionary)]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        public void ShouldRemove(CachingDictionaryType cachingDictionaryType)
        {
            var flushCacheTo = new ComposableDictionary<string, int>();
            flushCacheTo.Add("Hi", 2);
            var uut = CreateUnitUnderTest<string, int>(cachingDictionaryType, flushCacheTo);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);
            
            uut.TryRemove("Hi", out result).Should().BeTrue();
            result.Should().Be(2);

            flushCacheTo.TryGetValue("Hi", out result).Should().BeTrue();
            result.Should().Be(2);

            uut.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);

            uut.FlushCache();
            
            flushCacheTo.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);
        }
        
        [TestMethod]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionary)]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        public void ShouldNeverAddIfRemovedBeforeFlush(CachingDictionaryType cachingDictionaryType)
        {
            var flushCacheTo = new ComposableDictionary<string, int>();
            var uut = CreateUnitUnderTest<string, int>(cachingDictionaryType, flushCacheTo);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);

            flushCacheTo.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);

            uut.TryRemove("Hi", out result).Should().BeTrue();
            result.Should().Be(2);

            uut.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);

            uut.FlushCache();

            flushCacheTo.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);
        }
        
        [TestMethod]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionary)]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        public void ShouldAdd(CachingDictionaryType cachingDictionaryType)
        {
            var flushCacheTo = new ComposableDictionary<string, int>();
            var uut = CreateUnitUnderTest<string, int>(cachingDictionaryType, flushCacheTo);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);

            flushCacheTo.TryGetValue("Hi", out result).Should().BeFalse();
            result.Should().Be(default);

            uut.FlushCache();

            uut.TryGetValue("Hi", out result).Should().BeTrue();
            result.Should().Be(2);

            flushCacheTo.TryGetValue("Hi", out result).Should().BeTrue();
            result.Should().Be(2);
        }
        
        [TestMethod]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionary)]
        [DataRow(CachingDictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        public void AddDuplicateUnderlyingValueShouldFail(CachingDictionaryType cachingDictionaryType)
        {
            var flushCacheTo = new ComposableDictionary<string, int>();
            flushCacheTo.Add("Hi", 2);
            var uut = CreateUnitUnderTest<string, int>(cachingDictionaryType, flushCacheTo);
            Action action = () => uut.Add("Hi", 2);
            action.Should().Throw<AddFailedBecauseKeyAlreadyExistsException>();
        }
    }
}