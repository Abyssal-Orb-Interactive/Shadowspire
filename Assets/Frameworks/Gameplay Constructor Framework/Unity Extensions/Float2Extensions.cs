using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Float2Extensions
    {
        private const float X_ISOMETRIC_OFFSET = 0.5f;
        private const float Y_ISOMETRIC_OFFSET = 0.25f;
        private const float CARTESIAN_OFFSET = 2f;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToFloat3(this float2 float2)
        {
            return new float3(float2.x, float2.y, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ToCartesian(this float2 float2)
        {
            var x = float2.x - CARTESIAN_OFFSET * float2.y;
            var y = float2.x + CARTESIAN_OFFSET * float2.y;

            return new float2(x, y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ToIsometric(this float2 float2)
        {
            var isometricX = X_ISOMETRIC_OFFSET * (float2.x + float2.y);
            var isometricY = Y_ISOMETRIC_OFFSET * (float2.y - float2.x);

            return new float2(isometricX, isometricY);
        }
    }
}