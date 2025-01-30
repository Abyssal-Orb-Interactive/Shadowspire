using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace FloatExtensions
{
    public static class Float3Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Normalize(this float3 value)
        {
            return math.normalize(value);
        }
    }
}