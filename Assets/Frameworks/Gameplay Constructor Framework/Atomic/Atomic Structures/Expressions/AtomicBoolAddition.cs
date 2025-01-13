using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public sealed class AtomicBoolAddition : AtomicExpression<bool>
    {
        #region Constructors

        public AtomicBoolAddition() {}
        public AtomicBoolAddition(params Func<bool>[] members) : base(members) {}
        public AtomicBoolAddition(IEnumerable<Func<bool>> members) : base(members) {}

        #endregion

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            var count = members.Count;

            if (count == 0) return false;

            for (var i = 0; i < count; i++)
            {
                if (members[i].Invoke()) return true;
            }

            return false;
        }
    }
}