using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposableCollections.Dictionary.Interfaces;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using LiveLinq.Dictionary;

namespace ComposableCollections.GraphQL
{
    public class ObservableDictionarySubscription<TComposableDictionary, TKey, TValue> where TComposableDictionary : IObservableReadOnlyDictionary<TKey, TValue>
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<IDictionaryChangeStrict<TKey, TValue>>> Changes([Service] IResolverContext resolverContext)
        {
            var composableDictionary = resolverContext.Parent<TComposableDictionary>();
            return new AnonymousSourceStream<IDictionaryChangeStrict<TKey, TValue>>(() => ValueTask.CompletedTask,
                () => composableDictionary.ToLiveLinq().AsObservable().ToAsyncEnumerable());
        }

        private class AnonymousSourceStream<T> : ISourceStream<T>
        {
            private Func<ValueTask> _disposeAsync;
            private Func<IAsyncEnumerable<T>> _readEventsAsync;

            public AnonymousSourceStream(Func<ValueTask> disposeAsync, Func<IAsyncEnumerable<T>> readEventsAsync)
            {
                _disposeAsync = disposeAsync;
                _readEventsAsync = readEventsAsync;
            }

            public ValueTask DisposeAsync()
            {
                return _disposeAsync();
            }

            async IAsyncEnumerable<object> ISourceStream.ReadEventsAsync()
            {
                await foreach (var item in _readEventsAsync())
                {
                    yield return item;
                }
            }

            IAsyncEnumerable<T> ISourceStream<T>.ReadEventsAsync()
            {
                return _readEventsAsync();
            }
        }
    }
}