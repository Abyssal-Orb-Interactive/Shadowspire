using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public sealed class AtomicBoolMultiplication : AtomicExpression<bool>
    {
        #region Constructors
        
        public AtomicBoolMultiplication() {}
        public AtomicBoolMultiplication(params Func<bool>[] members) : base(members) {}
        public AtomicBoolMultiplication(IEnumerable<Func<bool>> members) : base(members) {}
        
        #endregion

        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            var count = members.Count;

            if (count == 0) return true;

            for (var i = 0; i < count; i++)
            {
                if (!members[i].Invoke()) return false;
            }

            return true;
        }
    }
    
    public class AtomicBoolMultiplicationWithIntArg : AtomicExpression<int ,bool>
    {
        #region Constructors
        
        public AtomicBoolMultiplicationWithIntArg() {}
        public AtomicBoolMultiplicationWithIntArg(params Func<int, bool>[] members) : base(members) {}
        public AtomicBoolMultiplicationWithIntArg(IEnumerable<Func<int, bool>> members) : base(members) {}
        
        #endregion

        protected override bool Invoke(IReadOnlyList<Func<int, bool>> members, int arg)
        {
            var count = members.Count;

            if (count == 0) return true;

            for (var i = 0; i < count; i++)
            {
                if (!members[i].Invoke(arg)) return false;
            }

            return true;
        }
    }
}