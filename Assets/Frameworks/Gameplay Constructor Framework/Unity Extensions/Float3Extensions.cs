using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Float3Extensions
    {
        private const float X_ISOMETRIC_OFFSET = 0.5f;
        private const float Y_ISOMETRIC_OFFSET = 0.25f;
        private const float CARTESIAN_OFFSET = 2f;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToCartesian(this float3 float3)
        {
            var x = float3.x - CARTESIAN_OFFSET * float3.y;
            var y = float3.x + CARTESIAN_OFFSET * float3.y;

            return new float3(x, y, float3.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToIsometric(this float3 float3)
        {
            var isometricX = X_ISOMETRIC_OFFSET * (float3.x + float3.y);
            var isometricY = Y_ISOMETRIC_OFFSET * (float3.y - float3.x);

            return new float3(isometricX, isometricY, float3.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ToInt3(this float3 float3)
        {
            var x = Mathf.RoundToInt(float3.x);
            var y = Mathf.RoundToInt(float3.y);
            var z = Mathf.RoundToInt(float3.z);

            return new int3(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 FloorToInt3(this float3 float3)
        {
            var x = Mathf.FloorToInt(float3.x);
            var y = Mathf.FloorToInt(float3.y);
            var z = Mathf.FloorToInt(float3.z);

            return new int3(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 CeilToInt3(this float3 float3)
        {
            var x = Mathf.RoundToInt(float3.x);
            var y = Mathf.CeilToInt(float3.y);
            var z = Mathf.CeilToInt(float3.z);

            return new int3(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ToInt2(this float3 float3)
        {
            var x = Mathf.RoundToInt(float3.x);
            var y = Mathf.RoundToInt(float3.y);

            return new int2(x, y);
        }
    }
}