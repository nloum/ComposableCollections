using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ComposableCollections.List
{
    public class AggregateOrderedList<T> : IOrderedList<T>
    {
        protected ImmutableList<IOrderedList<T>> Lists { get; set; } = ImmutableList<IOrderedList<T>>.Empty;
        public ImmutableList<IOrderedList<T>> InternalLists => Lists;

        public void Add(IOrderedList<T> list)
        {
            Lists = Lists.Add(list);
        }

        public void Remove(IOrderedList<T> list)
        {
            Lists = Lists.Remove(list);
        }

        public virtual void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach(var list in Lists)
            {
                list.Clear();
            }
        }

        public bool Contains(T item)
        {
            foreach(var list in Lists)
            {
                if (list.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                if (arrayIndex >= array.Length)
                {
                    break;
                }
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            foreach(var list in Lists)
            {
                if (list.Remove(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsReadOnly => false;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Compare(T item1, T item2)
        {
            return Lists[0].Compare(item1, item2);
        }

        public IOrderedList<T> ThenBy(Func<T, T, int> comparer)
        {
            var result = new AggregateOrderedList<T>();
            foreach (var list in Lists)
            {
                result.Add(list.ThenBy(comparer));
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerators = Lists.Select( enumerable => enumerable.GetEnumerator() ).ToList();

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

        public int Count => Lists.Select(list => list.Count).Sum();
    }
}