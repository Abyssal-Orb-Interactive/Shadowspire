using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace ReactiveLibraryFacade.DataStructures
{
    public struct DisposableBag : IDisposable
    {
        [CanBeNull] private IDisposable[] _disposables;
        private int _count;
        private bool _isDisposed;

        public DisposableBag(int capacity)
        {
            _disposables = new IDisposable[capacity];
            _count = 0;
            _isDisposed = false;
        }
        
        public DisposableBag(params IDisposable[] disposables)
        {
            _disposables = disposables;
            _count = _disposables.Length;
            _isDisposed = false;
        }
        
        public DisposableBag(IEnumerable<IDisposable> disposables)
        {
            _disposables = disposables.ToArray();
            _count = _disposables.Length;
            _isDisposed = false;
        }

        public void Add(IDisposable disposable)
        {
            if (_isDisposed)
            {
                disposable.Dispose();
                return;
            }

            if (_disposables == null) _disposables = new IDisposable[4];
            else if(_count == _disposables.Length) Array.Resize(ref _disposables, _count * 2);

            _disposables[_count++] = disposable;
        }

        public void Clear()
        {
            if (_disposables == null) return;
            
            for (var i = 0; i < _count; i++)
            {
                _disposables[i]?.Dispose();
            }

            _disposables = null;
            _count = 0;
        }
        
        public void Dispose()
        {
            Clear();
            _isDisposed = true;
        }
    }
}