using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SimpleMonads;
using static MoreCollections.Utility;

namespace MoreCollections
{
    /// <summary>
    ///     This class provides a dictionary that can be bound to a WPF control.
    /// </summary>
    public class BindableDictionary<TKey, TValue> : IReadOnlyBindableDictionary<TKey, TValue>,
                                                      IDictionary<TKey, TValue>,
                                                      IReadOnlyDictionary<TKey, TValue>,
                                                      ICollection<IKeyValuePair<TKey, TValue>>,
                                                      ICollection
    {
        public IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> keyValuePairs) {
            foreach(var keyValuePair in keyValuePairs) {
                Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
        
        public void RemoveRange(IEnumerable<TKey> keys) {
            foreach(var key in keys) {
                Remove(key);
            }
        }
        
        // ************************************************************************
        // IReadOnlyDictionary<TKey, TValue> Members
        // ************************************************************************
    
        #region IReadOnlyDictionary<TKey, TValue> Members
    
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _keys;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _values;

        #endregion IReadOnlyDictionary<TKey, TValue> Members

        // ************************************************************************
        // IEnumerable<IKeyValuePair<TKey, TValue>> Members
        // ************************************************************************

        #region IEnumerable<IKeyValuePair<TKey, TValue>> Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A IEnumerator<T> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _masterList.GetEnumerator();
        }

        #endregion IEnumerable<IKeyValuePair<TKey, TValue>> Members

        // ************************************************************************
        // IEnumerable Members
        // ************************************************************************

        #region IEnumerable Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An IEnumerator object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Members

        // ************************************************************************
        // Private Fields
        // ************************************************************************

        #region Private Fields

        /// <summary>
        ///     A dictionary of link list nodes to work out for the key the corresponding
        ///     index for the master list, key list, and value list.
        /// </summary>
        protected Dictionary<TKey, DoubleLinkListIndexNode> _keyToIndex;

        /// <summary>
        ///     An observable list of key value pairs
        /// </summary>
        protected ObservableCollection<IKeyValuePair<TKey, TValue>> _masterList;

        /// <summary>
        ///     The last node of the link list, used for adding new nodes to the end
        /// </summary>
        protected DoubleLinkListIndexNode _lastNode;

        /// <summary>
        ///     The list of keys for the keys property
        /// </summary>
        protected KeyCollection<TKey, TValue> _keys;

        /// <summary>
        ///     The list of values for the values property
        /// </summary>
        protected ValueCollection<TKey, TValue> _values;

        #endregion Private Fields

        // ************************************************************************
        // Public Methods
        // ************************************************************************

        #region Public Methods

        /// <summary>
        ///     Initializes a new instance of this class that is empty, has the default
        ///     initial capacity, and uses the default equality comparer for the key
        ///     type.
        /// </summary>
        public BindableDictionary()
        {
            _keyToIndex = new Dictionary<TKey, DoubleLinkListIndexNode>();
            _masterList = new ObservableCollection<IKeyValuePair<TKey, TValue>>();
            _masterList.CollectionChanged += this.masterList_CollectionChanged;

            _keys = new KeyCollection<TKey, TValue>(this);
            _values = new ValueCollection<TKey, TValue>(this);
        }

        /// <summary>
        ///     Initializes a new instance of this class that contains elements copied
        ///     from the specified IDictionary
        ///     <TKey, TValue>
        ///         and uses the default
        ///         equality comparer for the key type.
        /// </summary>
        /// <param name="source"></param>
        public BindableDictionary(IDictionary<TKey, TValue> source)
            : this()
        {
            foreach (var pair in source)
            {
                Add(pair);
            }
        }

        /// <summary>
        ///     Initializes a new instance of this class that is empty, has the default
        ///     initial capacity, and uses the specified IEqualityComparer<T>.
        /// </summary>
        /// <param name="equalityComparer"></param>
        public BindableDictionary(IEqualityComparer<TKey> equalityComparer)
            : this()
        {
            _keyToIndex = new Dictionary<TKey, DoubleLinkListIndexNode>(equalityComparer);
        }

        /// <summary>
        ///     Initializes a new instance of this class that is empty, has the
        ///     specified initial capacity, and uses the default equality comparer for
        ///     the key type.
        /// </summary>
        /// <param name="capactity"></param>
        public BindableDictionary(int capactity)
            : this()
        {
            _keyToIndex = new Dictionary<TKey, DoubleLinkListIndexNode>(capactity);
        }

        /// <summary>
        ///     Initializes a new instance of this class that contains elements copied
        ///     from the specified IDictionary
        ///     <TKey, TValue>
        ///         and uses the specified
        ///         IEqualityComparer<T>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="equalityComparer"></param>
        public BindableDictionary(IDictionary<TKey, TValue> source, IEqualityComparer<TKey> equalityComparer)
            : this(equalityComparer)
        {
            foreach (var pair in source)
            {
                Add(pair);
            }
        }

        /// <summary>
        ///     Initializes a new instance of this class that is empty, has the
        ///     specified initial capacity, and uses the specified
        ///     IEqualityComparer<T>.
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="equalityComparer"></param>
        public BindableDictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
            : this()
        {
            _keyToIndex = new Dictionary<TKey, DoubleLinkListIndexNode>(capacity, equalityComparer);
        }

        /// <summary>
        ///     Gets the array index of the key passed in.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int IndexOfKey(TKey key)
        {
            return _keyToIndex[key].Index;
        }

        /// <summary>
        ///     Tries to get the index of the key passed in. Returns true if succeeded
        ///     and false otherwise.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool TryGetIndexOf(TKey key, out int index)
        {
            DoubleLinkListIndexNode node;
            if (_keyToIndex.TryGetValue(key, out node))
            {
                index = node.Index;
                return true;
            }
            index = 0;
            return false;
        }

        #endregion Public Methods

        // ************************************************************************
        // Events, Triggers and Handlers
        // ************************************************************************

        #region Events, Triggers and Handlers

        /// <summary>
        ///     Handles when the internal key value list changes, and passes on the
        ///     message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void masterList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCollectionChanged(e);
        }

        /// <summary>
        ///     Triggers the CollectionChanged event in a way that it can be handled
        ///     by controls on a different thread.
        /// </summary>
        /// <param name="e"></param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            //if (CollectionChanged != null)
            //{
            //    CollectionChanged(this, e);
            //}
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                _masterList.CollectionChanged += value;
            }
            remove
            {
                _masterList.CollectionChanged -= value;
            }
        }

        #endregion Events, Triggers and Handlers

        // ************************************************************************
        // IDictionary<TKey, TValue> Members
        // ************************************************************************

        #region IDictionary<TKey, TValue> Members

        /// <summary>
        ///     Adds an element with the provided key and value to the IDictionary<TKey, TValue>.
        /// </summary>
        /// <param name="key">
        ///     The object to use as the key of the element to add.
        /// </param>
        /// <param name="value">
        ///     The object to use as the value of the element to add.
        /// </param>
        public virtual void Add(TKey key, TValue value)
        {
            var node = new DoubleLinkListIndexNode(_lastNode, _keyToIndex.Count);
            _keyToIndex.Add(key, node);
            _lastNode = node;
            _masterList.Add(KeyValuePair(key, value));
        }

        /// <summary>
        ///     Determines whether the IDictionary<TKey, TValue> contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key to locate in the IDictionary<TKey, TValue>.
        /// </param>
        /// <returns>
        ///     True if the IDictionary<TKey, TValue> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return _keyToIndex.ContainsKey(key);
        }

        /// <summary>
        ///     Gets an ICollection<T> containing the keys of the IDictionary<TKey, TValue>.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                return _keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionaryEx<TKey, TValue>.Values => Values;

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var result))
            {
                return result.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        /// <summary>
        ///     Removes the element with the specified key from the IDictionary<TKey, TValue>.
        /// </summary>
        /// <param name="key">
        ///     The key of the element to remove.
        /// </param>
        /// <returns>
        ///     True if the element is successfully removed; otherwise, false. This method also returns false if key was not found
        ///     in the original IDictionary<TKey, TValue>.
        /// </returns>
        public bool Remove(TKey key)
        {
            DoubleLinkListIndexNode node;
            if (_keyToIndex.TryGetValue(key, out node))
            {
                _masterList.RemoveAt(node.Index);
                if (node == _lastNode)
                {
                    _lastNode = node.Previous;
                }
                node.Remove();
            }
            return _keyToIndex.Remove(key);
        }

        /// <summary>
        ///     Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key whose value to get.
        /// </param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found; otherwise, the default
        ///     value for the type of the value parameter. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     True if the object that implements IDictionary
        ///     <TKey, TValue> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            DoubleLinkListIndexNode index;
            if (_keyToIndex.TryGetValue(key, out index))
            {
                value = _masterList[index.Index].Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        ///     Gets an ICollection<T> containing the values in the IDictionary<TKey, TValue>.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return _values;
            }
        }

        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key of the element to get or set.
        /// </param>
        /// <returns>
        ///     The element with the specified key.
        /// </returns>
        public TValue this[TKey key]
        {
            get
            {
                var index = _keyToIndex[key].Index;
                return _masterList[index].Value;
            }
            set
            {
                if (ContainsKey(key))
                {
                    Remove(key);
                }
                Add(key, value);
            }
        }

        #endregion IDictionary<TKey, TValue> Members

        // ************************************************************************
        // ICollection<IKeyValuePair<TKey, TValue>> Members
        // ************************************************************************

        #region ICollection<IKeyValuePair<TKey, TValue>> Members

        /// <summary>
        ///     Adds an item to the ICollection<T>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(IKeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            Add(KeyValuePair(pair.Key, pair.Value));
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            return Contains(KeyValuePair(pair.Key, pair.Value));
        }

        /// <summary>
        ///     Removes all items from the ICollection<T>.
        /// </summary>
        public void Clear()
        {
            _keyToIndex.Clear();
            _masterList.Clear();
            _lastNode = null;
        }

        /// <summary>
        ///     Determines whether the ICollection<T> contains a specific value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IKeyValuePair<TKey, TValue> item)
        {
            return _masterList.Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IKeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _masterList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the ICollection<T>.
        /// </summary>
        /// <param name="item">
        ///     The object to remove from the ICollection<T>.
        /// </param>
        /// <returns>
        ///     True if item was successfully removed from the ICollection
        ///     <T>; otherwise, false. This method also returns false if item is not found in the original ICollection<T>.
        /// </returns>
        public bool Remove(IKeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        /// <summary>
        ///     Gets the number of elements contained in the ICollection<T>.
        /// </summary>
        public int Count
        {
            get
            {
                return _masterList.Count;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the ICollection<T> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<IKeyValuePair<TKey, TValue>>)_masterList).IsReadOnly;
            }
        }

        #endregion ICollection<IKeyValuePair<TKey, TValue>> Members

        // ************************************************************************
        // ICollection Members
        // ************************************************************************

        #region ICollection Members

        /// <summary>
        ///     Copies the elements of the ICollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)_masterList).CopyTo(array, index);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            Clear();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (var i = arrayIndex; i < array.Length; i++)
            {
                array[i] = _masterList[i - arrayIndex].ToKeyValuePair();
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Contains(item))
            {
                return false;
            }
            Remove(item.Key);
            return true;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            foreach (var pair in _masterList)
            {
                yield return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether access to the ICollection is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return ((ICollection)_masterList).IsSynchronized;
            }
        }

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the ICollection.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return ((ICollection)_masterList).SyncRoot;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion ICollection Members
    }
}
