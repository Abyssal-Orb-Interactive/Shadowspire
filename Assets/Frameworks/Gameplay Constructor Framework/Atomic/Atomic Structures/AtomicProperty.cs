using System;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public sealed class AtomicProperty<T> : IAtomicVariable<T>
    {
        private Func<T> _getter = null;
        private Action<T> _setter = null;
        public T CurrentValue => _getter.Invoke();

        public T Value
        {
            get => _getter.Invoke();
            set => _setter.Invoke(value);
        }


        public AtomicProperty() {}

        public AtomicProperty(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }
    }
}