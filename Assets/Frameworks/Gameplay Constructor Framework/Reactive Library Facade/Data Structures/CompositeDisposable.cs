#nullable enable
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ReactiveLibraryFacade.DataStructures
{
    public sealed class CompositeDisposable : ICollection<IDisposable>, IDisposable
    {
        private const int SHRINK_THRESHOLD = 64;
        
        private readonly object _gate = null;

        private List<IDisposable?>? _disposables = null;
        private bool _isDisposed = false;
        private int _count = 0;

        public bool IsDisposed => Volatile.Read(ref _isDisposed);
        public bool IsReadOnly => false;
        
        public int Count
        {
            get
            {
                lock (_gate)
                {
                    return _count;
                }
            }
        }

        public CompositeDisposable()
        {
            _gate = new object();
            _disposables = new List<IDisposable?>();
            _isDisposed = false;
            _count = 0;
        }
        
        public CompositeDisposable(int capacity)
        {
            _gate = new object();
            _disposables = new List<IDisposable?>(capacity);
            _isDisposed = false;
            _count = _disposables.Count;
        }
        
        public CompositeDisposable(params IDisposable[] disposables)
        {
            _gate = new object();
            _disposables = new List<IDisposable?>(disposables);
            _isDisposed = false;
            _count = _disposables.Count;
        }
        
        public CompositeDisposable(IEnumerable<IDisposable> disposables)
        {
            _gate = new object();
            _disposables = new List<IDisposable?>(disposables);
            _isDisposed = false;
            _count = _disposables.Count;
        }

        public void Add(IDisposable disposable)
        {
            lock (_gate)
            {
                if (!_isDisposed)
                {
                    _count += 1;
                    _disposables.Add(disposable);
                    return;
                }
            }        
            
            disposable.Dispose();
        }
        
        public bool Remove(IDisposable disposable)
        {
            lock (_gate)
            {
                if (_isDisposed) return false;

                var current = _disposables;

                var index = current.IndexOf(disposable);
                
                if (index == -1) return false;
                
                current[index] = null;

                if (current.Capacity > SHRINK_THRESHOLD && _count < current.Capacity / 2)
                {
                    var fresh = new List<IDisposable?>(current.Capacity / 2);
                    
                    fresh.AddRange(current.Where(d => d != null));

                    _disposables = fresh;
                }

                _count -= 1;
            }

            disposable.Dispose();
            return true;        
        }

        public bool Contains(IDisposable item)
        {
            lock (_gate)
            {
                return !_isDisposed && _disposables.Contains(item);
            }        
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            lock (_gate)
            {
                if (_isDisposed) return;

                if (arrayIndex + _count > array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex));
                }

                var i = 0;
                
                foreach (var item in _disposables.Where(item => item != null))
                {
                    array[arrayIndex + i++] = item;
                }
            }        
        }

        public void Clear()
        {
            IDisposable?[] targetDisposables;
            int clearCount;
            lock (_gate)
            {
                if (_isDisposed) return;
                if (_count == 0) return;

                var count = _disposables.Count;
                targetDisposables = ArrayPool<IDisposable?>.Shared.Rent(count);
                clearCount = count;

                _disposables.CopyTo(targetDisposables);

                _disposables.Clear();
                _count = 0;
            }

            try
            {
                foreach (var item in targetDisposables.AsSpan(0, clearCount))
                {
                    item?.Dispose();
                }
            }
            finally
            {
                ArrayPool<IDisposable?>.Shared.Return(targetDisposables, clearArray: true);
            }       
        }
        
        public IEnumerator<IDisposable> GetEnumerator()
        {
            lock (_gate)
            {
                return EnumerateAndClear(_disposables.ToArray()).GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<IDisposable> EnumerateAndClear(IDisposable?[] disposables)
        {
            try
            {
                foreach (var item in disposables)
                {
                    if (item != null)
                    {
                        yield return item;
                    }
                }
            }
            finally
            {
                disposables.AsSpan().Clear();
            }
        }
        
        public void Dispose()
        {
            List<IDisposable?> disposables;

            lock (_gate)
            {
                if (_isDisposed) return;

                _count = 0;
                _isDisposed = true;
                disposables = _disposables;
                _disposables = null!;
            }

            foreach (var item in disposables)
            {
                item?.Dispose();
            }
            
            disposables.Clear();
        }
    }
}