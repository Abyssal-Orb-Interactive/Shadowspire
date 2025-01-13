using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicStructures
{
    public class AtomicIntAddition : AtomicExpression<int>
    {
        #region Constructors

        public AtomicIntAddition() {}
        public AtomicIntAddition(params Func<int>[] summands) : base(summands) {}
        public AtomicIntAddition(IEnumerable<Func<int>> summands) : base(summands) {}

        #endregion

        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            var count = members.Count;
            var sum = 0;

            if (count == 0) return sum;

            for (var i = 0; i < count; i++)
            {
                sum += members[i].Invoke();
            }

            return sum;
        }
    }
}