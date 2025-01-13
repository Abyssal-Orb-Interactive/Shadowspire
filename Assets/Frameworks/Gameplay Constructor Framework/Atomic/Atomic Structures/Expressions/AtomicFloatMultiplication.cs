using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicFloatMultiplication : AtomicExpression<float>
    {
        #region Constructiors

        public AtomicFloatMultiplication() {}
        public AtomicFloatMultiplication(params Func<float>[] factors) : base(factors) {}
        public AtomicFloatMultiplication(IEnumerable<Func<float>> factors) : base(factors) {}

        #endregion

        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            var count = members.Count;
            var product = 1f;

            if (count == 0) return product;

            for (var i = 0; i < count; i++)
            {
                product *= members[i].Invoke();

                if (product == 0f) return product;
            }

            return product;
        }
    }
}