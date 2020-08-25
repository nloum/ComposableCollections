# ComposableCollections

ComposableCollections is a .NET library containing decorator and adapter classes that can be used to supercharge collection objects. Here are some examples:

```
var underlyingDictionary = new ComposableDictionary<int, string>();
var readWriteLockDictionaryDecorator = new ReadWriteLockDictionaryDecorator<int, string>(underlyingDictionary);
readWriteLockDictionaryDecorator.Add(3, "Hey");
Console.WriteLine(readWriteLockDictionaryDecorator[3]);
```

`ReadWriteLockDictionaryDecorator` takes another dictionary in its constructor and wraps it in a `ReadWriteSlimLock` so that every time a read method is called (like TryGetValue), the lock is locked in read mode, and every time a write method is called (like Add), the lock is locked in write mode. `ReadWriteSlimLock` allows multiple simultaneous operations if they're all reads but only one operation at a time if it's a write operation.

There's a fluent syntax for attaching decorators and adapters, and in this case that fluent syntax looks like this:

```
var readWriteLockDictionaryDecorator = new ComposableDictionary<int, string>()
    .WithReadWriteLock(); // this is the fluent syntax
readWriteLockDictionaryDecorator.Add(3, "Hey");
Console.WriteLine(readWriteLockDictionaryDecorator[3]);
```

Either syntax is valid. The more decorators and adapters you have on top of a collection, the bigger the difference between the two syntaxes.

---

- `DictionaryGetOrDefaultDecorator` takes another dictionary in its constructor wraps it so that if anybody tries to access an item that doesn't exist, you have the option of creating that element and returning it without the user of the `DictionaryGetOrDefaultDecorator` knowing that it didn't exist.

There are also adapters:

- `ConcurrentCachedWriteDictionaryAdapter` takes an underlying dictionary in its constructor and uses it for all read operations, but it caches write operations and then flushes them in order in a single method call to the underlying dictionary, on demand.
- `MappingDictionaryAdapter` takes an underlying dictionary with certain `TKey1` and `TValue1` types and transforms that underlying dictionary into a dictionary of type `TKey2` and `TValue2` using conversion methods that are passed into the `MappingDictinoaryAdapter` constructor, or overridden in a base class of `MappingDictionaryAdapter`.
