using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class TransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : ITransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformations;
        private readonly IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readWriteTransformations;

        public TransactionalTransformations(IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformations, IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readWriteTransformations)
        {
            _readOnlyTransformations = readOnlyTransformations;
            _readWriteTransformations = readWriteTransformations;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, IDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }
    }
}