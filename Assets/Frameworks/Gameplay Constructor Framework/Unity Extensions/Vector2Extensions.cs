using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Vector2Extensions
    {
        private const float X_ISOMETRIC_OFFSET = 0.5f;
        private const float Y_ISOMETRIC_OFFSET = 0.25f;
        private const float CARTESIAN_OFFSET = 2f;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToFloat3(this Vector2 vector2)
        {
            return new float3(vector2.x, vector2.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ToFloat2(this Vector2 vector2)
        {
            return new float2(vector2.x, vector2.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToCartesian(this Vector2 vector2)
        {
            var x = vector2.x - CARTESIAN_OFFSET * vector2.y;
            var y = vector2.x + CARTESIAN_OFFSET * vector2.y;

            return new Vector2(x, y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToIsometric(this Vector2 vector2)
        {
            var isometricX = X_ISOMETRIC_OFFSET * (vector2.x + vector2.y);
            var isometricY = Y_ISOMETRIC_OFFSET * (vector2.y - vector2.x);

            return new Vector2(isometricX, isometricY);
        }
    }
}