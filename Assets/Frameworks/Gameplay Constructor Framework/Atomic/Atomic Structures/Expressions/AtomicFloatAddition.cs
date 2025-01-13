using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicStructures
{
    public class AtomicFloatAddition : AtomicExpression<float>
    {
        #region Constructors

        public AtomicFloatAddition() {}
        public AtomicFloatAddition(params Func<float>[] summands) : base(summands) {}
        public AtomicFloatAddition(IEnumerable<Func<float>> summands) : base(summands) {}

        #endregion

        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            var count = members.Count;
            var sum = 0f;

            if (count == 0) return sum;

            for (var i = 0; i < count; i++)
            {
                sum += members[i].Invoke();
            }

            return sum;
        }
    }
}