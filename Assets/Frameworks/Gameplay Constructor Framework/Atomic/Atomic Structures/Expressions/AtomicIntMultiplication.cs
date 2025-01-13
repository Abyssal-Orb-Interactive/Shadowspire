using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicStructures
{
    public class AtomicIntMultiplication : AtomicExpression<int>
    {
        #region Constructiors

        public AtomicIntMultiplication() {}
        public AtomicIntMultiplication(params Func<int>[] factors) : base(factors) {}
        public AtomicIntMultiplication(IEnumerable<Func<int>> factors) : base(factors) {}

        #endregion

        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            var count = members.Count;
            var product = 1;

            if (count == 0) return product;

            for (var i = 0; i < count; i++)
            {
                product *= members[i].Invoke();

                if (product == 0) return product;
            }

            return product;
        }
    }
}