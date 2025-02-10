using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;

namespace IntExtensions
{
    public static class IncreasingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IncreaseValueWithZeroLowerBoundBy(this int value, in int amount)
        {
            if (value < 0) return 0;
            if (amount <= 0) return value;
            
            return value + amount;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateIncreasedValueWithZeroLowerBoundBy(this IAtomicValue<int> value, in int amount)
        {
            return value.CurrentValue.IncreaseValueWithZeroLowerBoundBy(amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IncreaseValueBy(this IAtomicVariable<int> value, in int amount = 1)
        {
            value.Value = value.CalculateIncreasedValueWithZeroLowerBoundBy(amount);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IncreaseValueBy(this IAtomicVariable<int> value, in IAtomicValue<int> amount)
        {
            value.CalculateIncreasedValueWithZeroLowerBoundBy(amount.CurrentValue);
        }
    }
}