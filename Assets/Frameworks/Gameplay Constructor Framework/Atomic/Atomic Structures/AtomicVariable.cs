using System;
using UnityEngine;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        [SerializeField] private T _value = default;
        
        public T CurrentValue => _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public AtomicVariable(){}

        public AtomicVariable(T value)
        {
            _value = value;
        }
    }
}