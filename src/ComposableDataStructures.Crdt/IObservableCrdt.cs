using System;

namespace ComposableDataStructures.Crdt
{
    public interface IObservableCrdt<TWrite> : ICrdt<TWrite>, IObservable<TWrite>
    {
    }
}