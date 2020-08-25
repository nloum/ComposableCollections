using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Transactional;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithReadWriteLockTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformationsImpl ComposableDictionaryTransformations { get; }
        public static TransactionalTransformationsImpl TransactionalTransformations { get; }

        static WithReadWriteLockTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformationsImpl();
            TransactionalTransformations = new TransactionalTransformationsImpl();
        }

        public class TransactionalTransformationsImpl : ITransactionalTransformations<TKey, TValue, TKey, TValue, object>
        {
            public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly, new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })),
                    (readWrite, disposable) => new DisposableDictionaryAdapter<TKey, TValue>(readWrite, new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })));
            }

            public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly, new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })),
                    (readWrite, disposable) => new CachedDisposableDictionaryAdapter<TKey, TValue>(readWrite, new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })));
            }

            public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly, new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })),
                    (readWrite, disposable) => new DisposableQueryableDictionaryAdapter<TKey, TValue>(readWrite, new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })));
            }

            public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly, new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })),
                    (readWrite, disposable) => new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(readWrite, new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })));
            }
            
            
            
            
            
            
            
            
            public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly.AsDisposableReadOnlyDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readOnly.GetKey),
                    (readWrite, disposable) => new DisposableDictionaryAdapter<TKey, TValue>(readWrite.AsDisposableDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readWrite.GetKey));
            }

            public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly.AsDisposableReadOnlyDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readOnly.GetKey),
                    (readWrite, disposable) => new CachedDisposableDictionaryAdapter<TKey, TValue>(readWrite.AsCachedDisposableDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readWrite.GetKey));
            }

            public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly.AsDisposableQueryableReadOnlyDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readOnly.GetKey),
                    (readWrite, disposable) => new DisposableQueryableDictionaryAdapter<TKey, TValue>(readWrite.AsDisposableQueryableDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readWrite.GetKey));
            }

            public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
            {
                return new ReadWriteLockTransactionalDecorator<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>>(source,
                    (readOnly, disposable) => new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(readOnly.AsDisposableQueryableReadOnlyDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readOnly.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readOnly.GetKey),
                    (readWrite, disposable) => new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(readWrite.AsCachedDisposableQueryableDictionary(), new AnonymousDisposable(
                        () =>
                        {
                            readWrite.Dispose();
                            disposable.Dispose();
                        })).WithBuiltInKey(readWrite.GetKey));
            }
        }
        
        public class ComposableDictionaryTransformationsImpl : ComposableDictionaryTransformationsBase<TKey, TValue, object>
        {
            public override IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
            {
                return new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
            }
        }
    }
}