using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using GenericNumbers.Relational;

namespace MoreCollections
{
    public class MultidimensionalArray<T> : IMultidimensionalArray<T>, IComparable<IMultidimensionalArray<T>>
    {
        private readonly T[] _elements;
        public int[] Dimensions { get; }

        public MultidimensionalArray(MultidimensionalArray<T> source)
            : this(source.Dimensions)
        {
            source._elements.CopyTo(_elements, 0);
        }

        public MultidimensionalArray(int[] dimensions, params T[] elements)
            : this(dimensions)
        {
            elements.CopyTo(_elements, 0);
        }

        public MultidimensionalArray(int[] dimensions, IReadOnlyList<T> elements)
            : this(dimensions)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                _elements[i] = elements[i];
            }
        }

        public MultidimensionalArray(params int[] dimensions)
        {
            this.Dimensions = dimensions;

            var size = 1;
            foreach (var dimension in this.Dimensions)
            {
                size *= dimension;
            }

            this._elements = new T[size];
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _elements.AsEnumerable().GetEnumerator();
        }

        public IReadOnlyList<T> Elements(params int[] indicesRange)
        {
            return new SequentialElementsImpl(this.Indices(indicesRange), idxs => this[idxs]);
        }

        public IReadOnlyList<int[]> Indices(params int[] indicesRange)
        {
            return new SequentialIndicesImpl(Dimensions, indicesRange);
        }

        public T this[params int[] indices]
        {
            get
            {
                return this._elements[this.TranslateIndices(indices)];
            }
            protected set
            {
                this._elements[this.TranslateIndices(indices)] = value;
            }
        }

        protected int[] BackwardsTranslateIndices(int index)
        {
            var result = new List<int>();
            for (var i = this.Dimensions.Length - 1; i >= 0; i--)
            {
                var div = index / this.Dimensions[i];
                int remainder = (int)Math.Round(Math.IEEERemainder(index, this.Dimensions[i]));
                index = div;
                result.Add(remainder);
            }
            return result.ToArray();
        }

        protected int TranslateIndices(params int[] indices)
        {
            int answer = 0;
            int multiplier = 1;
            for (var i = 0; i < this.Dimensions.Length; i++)
            {
                answer += multiplier * indices[i];
                multiplier *= this.Dimensions[i];
            }
            
            return answer;
        }

        protected bool Equals(IMultidimensionalArray<T> other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((IMultidimensionalArray<T>)obj);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 31;

                foreach (var dimension in Dimensions)
                {
                    hashCode = hashCode ^ dimension.GetHashCode();
                }
                foreach (var element in _elements)
                {
                    hashCode = hashCode ^ element.GetHashCode();
                }

                return hashCode;
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IMultidimensionalArray<T> other)
        {
            foreach (var comparison in Dimensions.Zip(other.Dimensions, (a, b) => a.CompareTo(b)))
            {
                if (comparison != 0) return comparison;
            }
            foreach (var comparison in this.Zip(other, (a, b) => a.CompareTo(b)))
            {
                if (comparison != 0) return comparison;
            }
            return 0;
        }

        protected class SequentialElementsImpl : IReadOnlyList<T>
        {
            private readonly IReadOnlyList<int[]> indices;

            private readonly Func<int[], T> indexer;

            public SequentialElementsImpl(IReadOnlyList<int[]> indices, Func<int[], T> indexer)
            {
                this.indices = indices;
                this.indexer = indexer;
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<T> GetEnumerator()
            {
                return indices.Select(idxs => this.indexer(idxs)).GetEnumerator();
            }

            /// <summary>
            /// Gets the number of elements in the collection.
            /// </summary>
            /// <returns>
            /// The number of elements in the collection. 
            /// </returns>
            public int Count => indices.Count;

            /// <summary>
            /// Gets the element at the specified index in the read-only list.
            /// </summary>
            /// <returns>
            /// The element at the specified index in the read-only list.
            /// </returns>
            /// <param name="index">The zero-based index of the element to get. </param>
            public T this[int index] => this.indexer(this.indices[index]);
        }

        protected class SequentialIndicesImpl : IReadOnlyList<int[]>
        {
            private readonly int[] indicesRange;

            private readonly int[] dimensions;

            private readonly int varyBy;

            public SequentialIndicesImpl(int[] dimensions, int[] indicesRange)
            {
                this.indicesRange = indicesRange;
                this.dimensions = dimensions;
                this.varyBy = -1;
                for (var i = 0; i < indicesRange.Length; i++)
                {
                    if (indicesRange[i] == -1)
                    {
                        if (this.varyBy != -1)
                            throw new ArgumentException("Cannot vary by multiple indices");
                        this.varyBy = i;
                    }
                }
            }

            public int[] this[int i]
            {
                get
                {
                    var currentIndices = new int[indicesRange.Length];
                    Array.Copy(indicesRange, currentIndices, indicesRange.Length);
                    currentIndices[varyBy] = i;
                    return currentIndices;
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<int[]> GetEnumerator()
            {
                for (var i = 0; i < this.dimensions[varyBy]; i++)
                {
                    var currentIndices = new int[indicesRange.Length];
                    Array.Copy(indicesRange, currentIndices, indicesRange.Length);
                    currentIndices[varyBy] = i;
                    yield return currentIndices;
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Gets the number of elements in the collection.
            /// </summary>
            /// <returns>
            /// The number of elements in the collection. 
            /// </returns>
            public int Count => dimensions[varyBy];
        }
    }
}
