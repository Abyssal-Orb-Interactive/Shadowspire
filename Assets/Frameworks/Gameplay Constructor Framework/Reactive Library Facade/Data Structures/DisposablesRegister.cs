using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveLibraryFacade.DataStructures
{
    public sealed class DisposablesRegister<TKey> : IDictionary<TKey, IDisposable>, IDisposable
    {
        private Dictionary<TKey, IDisposable> _subscribes = null;

        public ICollection<TKey> Keys => _subscribes.Keys;
        public ICollection<IDisposable> Values => _subscribes.Values;
        public int Count => _subscribes.Count;
        public bool IsReadOnly => false;
        
        public DisposablesRegister()
        {
            _subscribes = new Dictionary<TKey, IDisposable>();
        }

        public DisposablesRegister(IReadOnlyDictionary<TKey, IDisposable> subscribes)
        {
            _subscribes = new Dictionary<TKey, IDisposable>(subscribes);
        }
        
        public IDisposable this[TKey key]
        {
            get => _subscribes[key];
            set
            {
                if (_subscribes.TryGetValue(key, out var oldSubscribe)) oldSubscribe?.Dispose();

                _subscribes[key] = value;
            }
        }

        public bool TryGetValue(TKey key, out IDisposable subscribe)
        {
            return _subscribes.TryGetValue(key, out subscribe);
        }

        public void Add(KeyValuePair<TKey, IDisposable> subscribeEntry)
        {
            var (key, value) = subscribeEntry;
            
            _subscribes.Add(key, value);
        }
        
        public void Add(TKey key, IDisposable subscribe)
        {
            _subscribes.Add(key, subscribe);
        }

        public void CopyTo(KeyValuePair<TKey, IDisposable>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index must be non-negative.");

            if (array.Length - arrayIndex < _subscribes.Count)
                throw new ArgumentException("The target array is too small to hold the elements.");

            foreach (var pair in _subscribes)
            {
                array[arrayIndex++] = pair;
            }
        }

        public bool Remove(KeyValuePair<TKey, IDisposable> subscribeEntry)
        {
            var (key, value) = subscribeEntry;
            
            value?.Dispose();
            return _subscribes.Remove(key);
        }

        public bool Remove(TKey key)
        {
            if (_subscribes.ContainsKey(key)) _subscribes[key]?.Dispose();

            return _subscribes.Remove(key);
        }

        public bool Contains(KeyValuePair<TKey, IDisposable> subscribeEntry)
        {
            return _subscribes.Contains(subscribeEntry);
        }
        
        public bool ContainsKey(TKey key)
        {
            return _subscribes.ContainsKey(key);
        }
        
        public void Clear()
        {
            Dispose();
            _subscribes.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, IDisposable>> GetEnumerator() 
        {
            return _subscribes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            foreach (var subscribe in _subscribes.Values)
            {
                subscribe?.Dispose();
            }
            
            Clear();
        }
    }
}