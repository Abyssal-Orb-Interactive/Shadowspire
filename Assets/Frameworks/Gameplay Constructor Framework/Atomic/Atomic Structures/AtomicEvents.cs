using System;
using System.Collections.Generic;
using ReactiveLibraryFacade.DataStructures;
using Sirenix.OdinInspector;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicEvent : IAtomicEvent
    {
        private Action _onEvent = null;
        private List<Action> _subscribers = null;

        public AtomicEvent()
        {
            _subscribers = new List<Action>();
        }
        
        public IDisposable Subscribe(Action subscriber)
        {
            _onEvent += subscriber;
            _subscribers.Add(subscriber);

            return new Disposable(() => Unsubscribe(subscriber));
        }
        
        private void Unsubscribe(Action action)
        {
            _onEvent -= action;
            _subscribers.Remove(action);
        }

        [Button]
        public virtual void Invoke()
        {
            _onEvent.Invoke();
        }

        public void Dispose()
        {
            _onEvent = null;
            _subscribers.Clear();
        }
    }

    [Serializable]
    public class AtomicEvent<T> : IAtomicEvent<T>
    {
        private Action<T> _onEvent = null;
        private List<Action<T>> _subscribers = new();

        public AtomicEvent() {}
        
        public AtomicEvent(Action<T> onEvent)
        {
            _onEvent = onEvent;
        }
        
        public IDisposable Subscribe(Action<T> action)
        {
            _subscribers ??= new List<Action<T>>();

            _onEvent += action;
            _subscribers.Add(action);

            return new Disposable(() => Unsubscribe(action));
        }

        private void Unsubscribe(Action<T> action)
        {
            _onEvent -= action;
            _subscribers.Remove(action);
        }
        
        [Button]
        public virtual void Invoke(T value)
        {
            _onEvent?.Invoke(value);
        }

        public void Dispose()
        {
            _onEvent = null;
            _subscribers?.Clear();
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2> : IAtomicEvent<T1, T2>
    {
        private Action<T1, T2> _onEvent = null;
        private List<Action<T1, T2>> _subscribers = null;

        public AtomicEvent()
        {
            _subscribers = new List<Action<T1, T2>>();
        }
        
        public IDisposable Subscribe(Action<T1, T2> action)
        {
            _subscribers ??= new List<Action<T1, T2>>();

            _onEvent += action;
            _subscribers.Add(action);
            
            return new Disposable(() => Unsubscribe(action));
        }

        private void Unsubscribe(Action<T1, T2> action)
        {
            _onEvent -= action;
            _subscribers.Remove(action);
        }
        
        [Button]
        public virtual void Invoke(T1 arg1, T2 arg2)
        {
            _onEvent?.Invoke(arg1, arg2);
        }

        public void Dispose()
        {
            _onEvent = null;
            _subscribers?.Clear();
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2, T3> : IAtomicEvent<T1, T2, T3>
    {
        private Action<T1, T2, T3> _onEvent = null;
        private List<Action<T1, T2, T3>> _subscribers = new();

        public IDisposable Subscribe(Action<T1, T2, T3> action)
        {
            _onEvent += action;
            _subscribers.Add(action);

            return new Disposable(() => Unsubscribe(action));
        }

        private void Unsubscribe(Action<T1, T2, T3> action)
        {
            _onEvent -= action;
            _subscribers.Remove(action);
        }
        
        [Button]
        public virtual void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            _onEvent?.Invoke(arg1, arg2, arg3);
        }

        public void Dispose()
        {
            _onEvent = null;
            _subscribers.Clear();
        }
    }
}