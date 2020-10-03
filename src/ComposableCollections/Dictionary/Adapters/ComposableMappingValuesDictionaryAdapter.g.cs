using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Interfaces;using System;using System.Collections.Generic;using ComposableCollections.Dictionary.Write;using ComposableCollections.Dictionary;
namespace ComposableCollections.Dictionary.Adapters {
public class ComposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IComposableDictionary<TKey, TValue> {
private readonly IComposableDictionary<TKey, TSourceValue> _adapted;
public ComposableMappingValuesDictionaryAdapter(IComposableDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public ComposableMappingValuesDictionaryAdapter(IComposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
public virtual void SetValue( TKey key,  TValue value) {
_adapted.SetValue( key,  value);
}

public virtual bool TryGetValue( TKey key,  out TValue value) {
return _adapted.TryGetValue( key,  out value);
}


public virtual void Write( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.Write.DictionaryWrite<TKey, TValue>> writes,  out System.Collections.Generic.IReadOnlyList<ComposableCollections.Dictionary.Write.DictionaryWriteResult<TKey, TValue>> results) {
_adapted.Write( writes,  out results);
}

public virtual bool TryAdd( TKey key,  TValue value) {
return _adapted.TryAdd( key,  value);
}

public virtual bool TryAdd( TKey key,  System.Func<TValue> value) {
return _adapted.TryAdd( key,  value);
}

public virtual bool TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _adapted.TryAdd( key,  value,  out existingValue,  out newValue);
}

public virtual void TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_adapted.TryAddRange( newItems,  out results);
}

public virtual void TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_adapted.TryAddRange( newItems,  out results);
}

public virtual void TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_adapted.TryAddRange( newItems,  key,  value,  out results);
}

public virtual void TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_adapted.TryAddRange( newItems);
}

public virtual void TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_adapted.TryAddRange( newItems);
}

public virtual void TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_adapted.TryAddRange( newItems,  key,  value);
}

public virtual void TryAddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_adapted.TryAddRange( newItems);
}

public virtual void TryAddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_adapted.TryAddRange( newItems);
}

public virtual void Add( TKey key,  TValue value) {
_adapted.Add( key,  value);
}

public virtual void AddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_adapted.AddRange( newItems);
}

public virtual void AddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_adapted.AddRange( newItems);
}

public virtual void AddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_adapted.AddRange( newItems,  key,  value);
}

public virtual void AddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_adapted.AddRange( newItems);
}

public virtual void AddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_adapted.AddRange( newItems);
}

public virtual bool TryUpdate( TKey key,  TValue value) {
return _adapted.TryUpdate( key,  value);
}

public virtual bool TryUpdate( TKey key,  TValue value,  out TValue previousValue) {
return _adapted.TryUpdate( key,  value,  out previousValue);
}

public virtual bool TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _adapted.TryUpdate( key,  value,  out previousValue,  out newValue);
}

public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_adapted.TryUpdateRange( newItems);
}

public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_adapted.TryUpdateRange( newItems);
}

public virtual void TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_adapted.TryUpdateRange( newItems,  key,  value);
}

public virtual void TryUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_adapted.TryUpdateRange( newItems);
}

public virtual void TryUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_adapted.TryUpdateRange( newItems);
}

public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.TryUpdateRange( newItems,  out results);
}

public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.TryUpdateRange( newItems,  out results);
}

public virtual void TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.TryUpdateRange( newItems,  key,  value,  out results);
}

public virtual void Update( TKey key,  TValue value) {
_adapted.Update( key,  value);
}

public virtual void UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_adapted.UpdateRange( newItems);
}

public virtual void UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_adapted.UpdateRange( newItems);
}

public virtual void UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_adapted.UpdateRange( newItems,  key,  value);
}

public virtual void Update( TKey key,  TValue value,  out TValue previousValue) {
_adapted.Update( key,  value,  out previousValue);
}

public virtual void UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.UpdateRange( newItems,  out results);
}

public virtual void UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.UpdateRange( newItems,  out results);
}

public virtual void UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_adapted.UpdateRange( newItems,  key,  value,  out results);
}

public virtual void UpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_adapted.UpdateRange( newItems);
}

public virtual void UpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_adapted.UpdateRange( newItems);
}

public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  TValue value) {
return _adapted.AddOrUpdate( key,  value);
}

public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _adapted.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}

public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _adapted.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}

public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_adapted.AddOrUpdateRange( newItems,  out results);
}

public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_adapted.AddOrUpdateRange( newItems,  out results);
}

public virtual void AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_adapted.AddOrUpdateRange( newItems,  key,  value,  out results);
}

public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_adapted.AddOrUpdateRange( newItems);
}

public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_adapted.AddOrUpdateRange( newItems);
}

public virtual void AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_adapted.AddOrUpdateRange( newItems,  key,  value);
}

public virtual void AddOrUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_adapted.AddOrUpdateRange( newItems);
}

public virtual void AddOrUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_adapted.AddOrUpdateRange( newItems);
}

public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_adapted.TryRemoveRange( keysToRemove);
}

public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_adapted.RemoveRange( keysToRemove);
}

public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_adapted.RemoveWhere( predicate);
}

public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_adapted.RemoveWhere( predicate);
}

public virtual void Clear() {
_adapted.Clear();
}

public virtual bool TryRemove( TKey key) {
return _adapted.TryRemove( key);
}

public virtual void Remove( TKey key) {
_adapted.Remove( key);
}

public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_adapted.TryRemoveRange( keysToRemove,  out removedItems);
}

public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_adapted.RemoveRange( keysToRemove,  out removedItems);
}

public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_adapted.RemoveWhere( predicate,  out removedItems);
}

public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_adapted.RemoveWhere( predicate,  out removedItems);
}

public virtual void Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_adapted.Clear( out removedItems);
}

public virtual bool TryRemove( TKey key,  out TValue removedItem) {
return _adapted.TryRemove( key,  out removedItem);
}

public virtual void Remove( TKey key,  out TValue removedItem) {
_adapted.Remove( key,  out removedItem);
}

}
}
