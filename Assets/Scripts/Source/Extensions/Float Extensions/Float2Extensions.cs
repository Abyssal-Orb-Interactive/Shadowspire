using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace FloatExtensions
{
    public static class Float2Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 Up()
        {
            return new float2(0f, 1f);
        }
    }
}