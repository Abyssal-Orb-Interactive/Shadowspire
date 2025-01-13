using System;

namespace ReactiveLibraryFacade.DataStructures
{
    public class Disposable : IDisposable
    {
        private Action _unsubscribe = null;

        public Disposable(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }

        public void Dispose()
        {
            _unsubscribe?.Invoke();
            _unsubscribe = null;
        }
    }
}