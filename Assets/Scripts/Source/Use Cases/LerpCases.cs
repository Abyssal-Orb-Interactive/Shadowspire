using System.Runtime.CompilerServices;

namespace UseCases
{
    public static class LerpCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateStepRatio(in float distance, in float maxStep)
        {
            if (maxStep <= 0f || distance <= 0f) return 0f;
            
            return distance > maxStep ? maxStep / distance : 1f;
        }
    }
}