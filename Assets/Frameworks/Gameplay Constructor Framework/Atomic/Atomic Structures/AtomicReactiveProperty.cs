using System;
using System.Collections.Generic;
using ReactiveLibraryFacade.DataStructures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicReactiveProperty<T> : IAtomicVariable<T>, IReadonlyAtomicReactiveProperty<T>
    {
        [OnValueChanged("OnValueChanged")]
        [SerializeField] private T _value = default;
        
        private Action<T> _onChanged = null;
        private List<Action<T>> _subscribers = null;

        public T CurrentValue => _value;
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChanged?.Invoke(_value);
            } 
        }

        public AtomicReactiveProperty()
        {
            _subscribers = new List<Action<T>>();
            _value = default;
        }

        public AtomicReactiveProperty(T value)
        {
            _subscribers = new List<Action<T>>();
            _value = value;
        }

        public IDisposable Subscribe(Action<T> subscriber)
        {
            _subscribers ??= new List<Action<T>>();
            
            _onChanged += subscriber;
            _subscribers.Add(subscriber);

            subscriber.Invoke(_value);

            return new Disposable(() => Unsubscribe(subscriber));
        }
        
        private void Unsubscribe(Action<T> action)
        {
            _onChanged -= action; 
            _subscribers.Remove(action);
        }
        
#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            _onChanged?.Invoke(value);
        }
#endif

        public void Dispose()
        {
            _onChanged = null;

            if(_subscribers == null) return;

            foreach (var subscriber in _subscribers)
            {
                _onChanged -= subscriber;
            }
            
            _subscribers.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj is AtomicReactiveProperty<T> another)
            {
                return CurrentValue.Equals(another.CurrentValue);
            }

            return false;
        }
    }
}