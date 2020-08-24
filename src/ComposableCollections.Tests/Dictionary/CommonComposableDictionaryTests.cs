using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests
{
    [TestClass]
    public class CommonComposableDictionaryTests
    {
        public enum DictionaryType
        {
            ConcurrentDictionary,
            ConcurrentCachingDictionaryWithMinimalState,
            ComposableDictionary,
            ConcurrentCachingDictionary,
        }
        
        private IComposableDictionary<TKey, TValue> CreateUnitUnderTest<TKey, TValue>(DictionaryType dictionaryType)
        {
            switch (dictionaryType)
            {
                case DictionaryType.ConcurrentDictionary:
                    return new ConcurrentDictionary<TKey, TValue>();
                case DictionaryType.ConcurrentCachingDictionaryWithMinimalState:
                    return new ConcurrentMinimalCachedStateDictionaryDecorator<TKey, TValue>(new ComposableDictionary<TKey, TValue>());
                case DictionaryType.ComposableDictionary:
                    return new ComposableDictionary<TKey, TValue>();
                case DictionaryType.ConcurrentCachingDictionary:
                    return new ConcurrentCachedDictionary<TKey, TValue>(new ComposableDictionary<TKey, TValue>(), new ComposableDictionary<TKey, TValue>());
                default:
                    throw new NotImplementedException();
            }
        }
        
        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldAdd(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);
            uut.TryGetValue("Hey", out result).Should().BeFalse();
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void AddShouldFailIfPreExistingKey(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);

            Action action = () => uut.Add("Hi", 3);
            action.Should().Throw<AddFailedBecauseKeyAlreadyExistsException>();
            
            uut.TryGetValue("Hi", out result).Should().BeTrue();
            result.Should().Be(2);
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldTryAdd(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            result.Should().Be(2);
            uut.TryAdd("Hi", 3).Should().BeFalse();
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldRemove(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(2);
            uut.Remove("Hi", out result);
            result.Should().Be(2);
            uut.TryGetValue("Hi", out result).Should().BeFalse();
            uut.ContainsKey("Hi").Should().BeFalse();
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void RemoveShouldFailIfNoSuchKey(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            Action action = () => uut.Remove("Hi", out var result);
            action.Should().Throw<RemoveFailedBecauseNoSuchKeyExistsException>();
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldTryRemove(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(2);
            uut.TryRemove("Hi", out result).Should().BeTrue();
            result.Should().Be(2);
            uut.TryGetValue("Hi", out result).Should().BeFalse();
            uut.ContainsKey("Hi").Should().BeFalse();
            uut.TryRemove("Hi", out result).Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldUpdate(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(2);
            uut.Update("Hi", 3);
            uut.TryGetValue("Hi", out result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(3);
        }
        
        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void UpdateShouldFailIfNoSuchKey(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            Action action = () => uut.Update("Hi", 3);
            action.Should().Throw<UpdateFailedBecauseNoSuchKeyExistsException>();
        }
        
        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldTryUpdate(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut.Add("Hi", 2);
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(2);
            uut.TryUpdate("Hi", 3).Should().BeTrue();
            uut.TryGetValue("Hi", out result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(3);
            uut.TryUpdate("Hey", 5).Should().BeFalse();
            uut.TryGetValue("Hey", out result).Should().BeFalse();
            uut.ContainsKey("Hey").Should().BeFalse();
        }

        [TestMethod]
        [DataRow(DictionaryType.ConcurrentDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionaryWithMinimalState)]
        [DataRow(DictionaryType.ComposableDictionary)]
        [DataRow(DictionaryType.ConcurrentCachingDictionary)]
        public void ShouldAddOrUpdate(DictionaryType dictionaryType)
        {
            var uut = CreateUnitUnderTest<string, int>(dictionaryType);
            uut["Hi"] = 2;
            uut.TryGetValue("Hi", out var result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(2);
            
            uut["Hi"] = 3;
            uut.TryGetValue("Hi", out result).Should().BeTrue();
            uut.ContainsKey("Hi").Should().BeTrue();
            result.Should().Be(3);
        }
    }
}