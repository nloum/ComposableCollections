using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    /// <summary>
    /// Contains extension methods and utility classes for the knapsack problem.
    /// </summary>
    public static class KnapsackProblem
    {
        /// <summary>
        ///     Implements a bounded knapsack algorithm.
        ///     Based on: http://rosettacode.org/wiki/Knapsack_problem/0-1#C.23
        /// Temporarily disabled because
        /// </summary>
        public static IEnumerable<T> Knapsack<T>(this IEnumerable<T> items, int maxWeightAllowed, Func<T, int> quantity,
            Func<T, int> quality)
        {
            return new KnapsackInternal<T>(items.ToList(), maxWeightAllowed, quantity, quality);
        }

        /// <summary>
        ///     A solution to the Knapsack problem.
        ///     Based on: http://rosettacode.org/wiki/Knapsack_problem/Continuous#C.23
        /// </summary>
        public static IEnumerable<ContinuousKnapsackItem<T>> ContinuousKnapsack<T>(this IEnumerable<T> items,
            double knapsackCapacity,
            Func<T, double> quantity, Func<T, double> quality)
        {
            double totalQuality = 0.0;

            foreach (T item in items.OrderByDescending(x => quality(x)/quantity(x)))
            {
                if (knapsackCapacity >= quantity(item))
                {
                    totalQuality += quality(item);
                    yield return new ContinuousKnapsackItem<T>(item, quantity(item));
                }
                else
                {
                    totalQuality += (quality(item)/quantity(item))*knapsackCapacity;
                    yield return new ContinuousKnapsackItem<T>(item, knapsackCapacity);
                }
                knapsackCapacity -= quantity(item);
                if (knapsackCapacity <= 0)
                    yield break;
            }
        }

        internal class KnapsackInternal<T> : IEnumerable<T>
        {
            private readonly int _maxWeightAllowed;
            private readonly Func<T, int> _value;
            private readonly Func<T, int> _weight;
            private readonly List<T> items;

            public KnapsackInternal(IList<T> source, int maxWeightAllowed, Func<T, int> weight, Func<T, int> value)
            {
                _weight = weight;
                _value = value;
                _maxWeightAllowed = maxWeightAllowed;
                items = new List<T>();

                foreach (T i in Sorte(source))
                {
                    AddItem(i);
                }
            }

            public int TotalWeight
            {
                get
                {
                    int sum = 0;
                    foreach (T item in this)
                    {
                        sum += _weight(item);
                    }
                    return sum;
                }
            }

            private void AddItem(T i)
            {
                if (TotalWeight + _weight(i) <= _maxWeightAllowed)
                    items.Add(i);
            }

            private IEnumerable<T> Sorte(IList<T> inputItems)
            {
                var choosenItems = new List<T>();
                for (int i = 0; i < inputItems.Count; i++)
                {
                    int j = -1;
                    if (i == 0)
                    {
                        choosenItems.Add(inputItems[i]);
                    }
                    if (i > 0)
                    {
                        if (!RecursiveF(inputItems, choosenItems, i, choosenItems.Count - 1, false, ref j))
                        {
                            choosenItems.Add(inputItems[i]);
                        }
                    }
                }
                return choosenItems;
            }

            private bool RecursiveF(IList<T> knapsackItems, IList<T> choosenItems, int i, int lastBound, bool dec,
                ref int indxToAdd)
            {
                if (!(lastBound < 0))
                {
                    if ((_weight(knapsackItems[i]) - _value(knapsackItems[i]))
                        < (_weight(choosenItems[lastBound]) - _value(choosenItems[lastBound])))
                    {
                        indxToAdd = lastBound;
                    }
                    return RecursiveF(knapsackItems, choosenItems, i, lastBound - 1, true, ref indxToAdd);
                }
                if (indxToAdd > -1)
                {
                    choosenItems.Insert(indxToAdd, knapsackItems[i]);
                    return true;
                }
                return false;
            }

            #region IEnumerable<Item> Members

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                foreach (T item in items)
                    yield return item;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return items.GetEnumerator();
            }

            #endregion
        }

        public sealed class ContinuousKnapsackItem<T>
        {
            internal ContinuousKnapsackItem(T item, double quantity)
            {
                Item = item;
                Quantity = quantity;
            }

            public T Item { get; private set; }
            public double Quantity { get; private set; }

            public override string ToString()
            {
                return string.Format("{0} of {1}", Quantity, Item);
            }
        }
    }
}
