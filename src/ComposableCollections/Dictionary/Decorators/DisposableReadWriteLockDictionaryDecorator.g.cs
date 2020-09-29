using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Decorators {
public class DisposableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockDictionaryDecorator<TKey, TValue>, IDisposableDictionary<TKey, TValue> {
private readonly IDisposableDictionary<TKey, TValue> _adapted;
public DisposableReadWriteLockDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
