using System;
using System.Collections.Generic;

namespace ComposableDataStructures.Crdt
{
    public class Summation<T> : ICrdt<T>
    {
        private object _lock = new object();
        private readonly Func<T, T, T> _add;

        public T State { get; private set; }

        public Summation(T initialState, Func<T, T, T> add)
        {
            State = initialState;
            _add = add;
        }

        protected Summation(T initialState)
        {
        }
        
        protected virtual T Add(T t1, T t2)
        {
            return _add(t1, t2);
        }
        
        public void Write(IEnumerable<T> writes)
        {
            lock (_lock)
            {
                foreach (var write in writes)
                {
                    State = Add(State, write);
                }
            }
        }
    }
}