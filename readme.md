# ComposableCollections

ComposableCollections is a .NET library containing decorator and adapter classes that can be used to supercharge collection objects. Here are some examples:

- `ReadWriteLockDictionaryDecorator` takes another dictionary in its constructor and wraps it in a `ReadWriteSlimLock` so that every time a read method is called (like TryGetValue), the lock is locked in read mode, and every time a write method is called (like Add), the lock is locked in write mode. `ReadWriteSlimLock` allows multiple simultaneous operations if they're all reads but only one operation at a time if it's a write operation.
- `DictionaryGetOrDefaultDecorator` takes another dictionary in its constructor wraps it so that if anybody tries to access an item that doesn't exist, you have the option of creating that element and returning it without the user of the `DictionaryGetOrDefaultDecorator` knowing that it didn't exist.

