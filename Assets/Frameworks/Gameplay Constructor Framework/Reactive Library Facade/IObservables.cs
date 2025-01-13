using System;

namespace ReactiveLibraryFacade
{
    public interface IObservable : IDisposable
    {
        public IDisposable Subscribe(Action observer);

    }
    
    public interface IObservable<out T> : IDisposable
    {
        public IDisposable Subscribe(Action<T> observer);

    }
    public interface IObservable<out T1, out T2> : IDisposable
    {
        public IDisposable Subscribe(Action<T1, T2> observer);

    }
    
    public interface IObservable<out T1, out T2, out T3> : IDisposable
    {
        public IDisposable Subscribe(Action<T1, T2, T3> observer);

    }
}