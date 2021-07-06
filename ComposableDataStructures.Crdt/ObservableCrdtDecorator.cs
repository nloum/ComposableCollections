using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace ComposableDataStructures.Crdt
{
    public class ObservableCrdtDecorator<TWrite> : IObservableCrdt<TWrite>
    {
        private readonly ICrdt<TWrite> _state;
        private readonly Subject<TWrite> _writes = new Subject<TWrite>();

        public ObservableCrdtDecorator(ICrdt<TWrite> state)
        {
            _state = state;
        }

        public void Write(IEnumerable<TWrite> writes)
        {
            _state.Write(writes.Select(x =>
            {
                _writes.OnNext(x);
                return x;
            }));
        }

        public IDisposable Subscribe(IObserver<TWrite> observer)
        {
            return _writes;
        }
    }
}