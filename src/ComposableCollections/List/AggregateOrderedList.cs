using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComposableCollections.List
{
    public class AggregateOrderedList<T> : IReadOnlyCollection<T>
    {
        private ImmutableList<OrderedList<T>> _lists = ImmutableList<OrderedList<T>>.Empty;

        public void Add(OrderedList<T> list)
        {
            _lists = _lists.Add(list);
        }

        public void Remove(OrderedList<T> list)
        {
            _lists = _lists.Remove(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Compare(T item1, T item2)
        {
            return _lists[0].Compare(item1, item2);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            var enumerators = _lists.Select( enumerable => enumerable.GetEnumerator() ).ToList();

            var enumeratorsToRemove = new List<int>();
            for (var i = 0; i < enumerators.Count; i++)
            {
                var enumerator = enumerators[i];
                if (!enumerator.MoveNext())
                {
                    enumeratorsToRemove.Insert(0, i);
                }
            }

            foreach (var idx in enumeratorsToRemove)
            {
                enumerators.RemoveAt(idx);
            }

            while (true)
            {
                if (enumerators.Count == 0)
                {
                    break;
                }

                IEnumerator<T> minimumSoFar = enumerators[0];
                var minimumSoFarIndex = 0;
                for (var i = 1; i < enumerators.Count; i++)
                {
                    var currentEnumerator = enumerators[i];
                    var comparisonResult = Compare(minimumSoFar.Current, currentEnumerator.Current);
                    if (comparisonResult > 0)
                    {
                        minimumSoFar = currentEnumerator;
                        minimumSoFarIndex = i;
                    }
                }

                yield return minimumSoFar.Current;
                if (!minimumSoFar.MoveNext())
                {
                    enumerators.RemoveAt(minimumSoFarIndex);
                }
            }
        }

        public int Count => _lists.Select(list => list.Count).Sum();
    }
}