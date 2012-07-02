using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Practice.Types.Interface;

namespace Practice.Pattern
{
    /// <summary>
    /// The thread safe dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class ThreadSafeDictionary<TKey, TValue> : IThreadSafeDictionary<TKey, TValue>
    {
        /// <summary>
        /// This is the internal dictionary that we are wrapping
        /// </summary>
        private readonly IDictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        /// <summary>
        /// The dictionary lock.
        /// </summary>
        private readonly ReaderWriterLockSlim dictionaryLock = Locks.GetLockInstance(LockRecursionPolicy.NoRecursion);

        // setup the lock;

        #region IThreadSafeDictionary<TKey,TValue> Members

        /// <summary>
        /// This is a blind remove. Prevents the need to check for existence first.
        /// </summary>
        /// <param name="key"> Key to remove </param>
        public void RemoveSafe(TKey key)
        {
            using (new ReadLock(dictionaryLock))
            {
                if (dict.ContainsKey(key))
                {
                    using (new WriteLock(dictionaryLock))
                    {
                        dict.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Merge does a blind remove, and then add.  Basically a blind Upsert.  
        /// </summary>
        /// <param name="key"> Key to lookup </param>
        /// <param name="newValue"> New Value </param>
        public void MergeSafe(TKey key, TValue newValue)
        {
            using (new WriteLock(dictionaryLock))
            {
                // take a writelock immediately since we will always be writing
                if (dict.ContainsKey(key))
                {
                    dict.Remove(key);
                }


                dict.Add(key, newValue);
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> The remove. </returns>
        public virtual bool Remove(TKey key)
        {
            using (new WriteLock(dictionaryLock))
            {
                return dict.Remove(key);
            }
        }

        /// <summary>
        /// The contains key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> The contains key. </returns>
        public virtual bool ContainsKey(TKey key)
        {
            using (new ReadOnlyLock(dictionaryLock))
            {
                return dict.ContainsKey(key);
            }
        }

        /// <summary>
        /// The try get value.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <param name="value"> The value. </param>
        /// <returns> The try get value. </returns>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            using (new ReadOnlyLock(dictionaryLock))
            {
                return dict.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key"> The key. </param>
        public virtual TValue this[TKey key]
        {
            get
            {
                using (new ReadOnlyLock(dictionaryLock))
                {
                    return dict[key];
                }
            }

            set
            {
                using (new WriteLock(dictionaryLock))
                {
                    dict[key] = value;
                }
            }
        }

        /// <summary>
        /// Gets Keys.
        /// </summary>
        public virtual ICollection<TKey> Keys
        {
            get
            {
                using (new ReadOnlyLock(dictionaryLock))
                {
                    return new List<TKey>(dict.Keys);
                }
            }
        }

        /// <summary>
        /// Gets Values.
        /// </summary>
        public virtual ICollection<TValue> Values
        {
            get
            {
                using (new ReadOnlyLock(dictionaryLock))
                {
                    return new List<TValue>(dict.Values);
                }
            }
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public virtual void Clear()
        {
            using (new WriteLock(dictionaryLock))
            {
                dict.Clear();
            }
        }

        /// <summary>
        /// Gets Count.
        /// </summary>
        public virtual int Count
        {
            get
            {
                using (new ReadOnlyLock(dictionaryLock))
                {
                    return dict.Count;
                }
            }
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> The contains. </returns>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            using (new ReadOnlyLock(dictionaryLock))
            {
                return dict.Contains(item);
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item"> The item. </param>
        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            using (new WriteLock(dictionaryLock))
            {
                dict.Add(item);
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <param name="value"> The value. </param>
        public virtual void Add(TKey key, TValue value)
        {
            using (new WriteLock(dictionaryLock))
            {
                dict.Add(key, value);
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> The remove. </returns>
        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            using (new WriteLock(dictionaryLock))
            {
                return dict.Remove(item);
            }
        }

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="array"> The array. </param>
        /// <param name="arrayIndex"> The array index. </param>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            using (new ReadOnlyLock(dictionaryLock))
            {
                dict.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsReadOnly.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get
            {
                using (new ReadOnlyLock(dictionaryLock))
                {
                    return dict.IsReadOnly;
                }
            }
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotSupportedException(
                "Cannot enumerate a threadsafe dictionary.  Instead, enumerate the keys or values collection");
        }


        /// <summary>
        /// The i enumerable. get enumerator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException(
                "Cannot enumerate a threadsafe dictionary.  Instead, enumerate the keys or values collection");
        }

        #endregion
    }
}