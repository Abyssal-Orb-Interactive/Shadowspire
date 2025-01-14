using System.Runtime.CompilerServices;
using FloatExtensions;
using UnityEngine;

namespace UseCases
{
    public static class TimeCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetUpElapsedTimeByDeltaTime(ref float timer)
        {
            timer = timer.CalculateElapsedTimeBy(Time.deltaTime);
        }
    }
}