using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public abstract class AtomicExpression<T> : IAtomicExpression<T>
    {
        private readonly List<Func<T>> _members = null;

        public T CurrentValue => Invoke();

        public AtomicExpression()
        {
            _members = new List<Func<T>>();
        }

        public AtomicExpression(params Func<T>[] members)
        {
            _members = members.ToList();
        }

        public AtomicExpression(IEnumerable<Func<T>> members)
        {
            _members = members.ToList();
        }
        
        public void Append(Func<T> member)
        {
            _members.Add(member);
        }

        public void Remove(Func<T> member)
        {
            _members.Remove(member);
        }

        public bool Has(Func<T> member)
        {
            return _members.Contains(member);
        }

        [Button]
        public virtual T Invoke()
        {
            return Invoke(_members);
        }

        protected abstract T Invoke(IReadOnlyList<Func<T>> members);
    }
    
    public abstract class AtomicExpression<T, R> : IAtomicExpression<T, R>
    {
        private readonly List<Func<T, R>> _members = new();
        
        public AtomicExpression()
        {
            _members = new List<Func<T, R>>();
        }

        public AtomicExpression(params Func<T, R>[] members)
        {
            _members = members.ToList();
        }

        public AtomicExpression(IEnumerable<Func<T, R>> members)
        {
            _members = members.ToList();
        }

        public void Append(Func<T, R> member)
        {
            _members.Add(member);
        }

        public void Remove(Func<T, R> member)
        {
            _members.Remove(member);
        }

        public bool Has(Func<T, R> member)
        {
            return _members.Contains(member);
        }

        [Button]
        public R Invoke(T args)
        {
            return Invoke(_members, args);
        }

        protected abstract R Invoke(IReadOnlyList<Func<T, R>> members, T args);
    }
}