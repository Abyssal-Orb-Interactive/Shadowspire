using System.Runtime.CompilerServices;

namespace FloatExtensions
{
    public static class TimerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateElapsedTimeBy(this float timer, float timeDelta)
        {
            if (timer < 0f) return 0f;
            if(timeDelta <= 0f) return timer;
            
            return timer + timeDelta;
        }
    }
}