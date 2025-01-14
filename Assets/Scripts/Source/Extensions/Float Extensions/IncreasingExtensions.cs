using System.Runtime.CompilerServices;

namespace FloatExtensions
{
    public static class IncreasingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateIncreasedValueWithZeroLowerBoundBy(this float value, in float operand)
        {
            if (value < 0f) return 0f;
            if (operand <= 0f) return value;
            
            return value + operand;
        }
    }
}