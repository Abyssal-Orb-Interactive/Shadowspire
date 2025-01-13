using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Vector3Extensions
    {
        private const float X_ISOMETRIC_OFFSET = 0.5f;
        private const float Y_ISOMETRIC_OFFSET = 0.25f;
        private const float CARTESIAN_OFFSET = 2f;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToFloat3(this Vector3 vector3)
        {
            return new float3(vector3.x, vector3.y, vector3.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ToFloat2(this Vector3 vector3)
        {
            return new float2(vector3.x, vector3.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToCartesian(this Vector3 vector3)
        {
            var x = vector3.x - CARTESIAN_OFFSET * vector3.y;
            var y = vector3.x + CARTESIAN_OFFSET * vector3.y;

            return new Vector3(x, y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToIsometric(this Vector3 vector3)
        {
            var isometricX = X_ISOMETRIC_OFFSET * (vector3.x + vector3.y);
            var isometricY = Y_ISOMETRIC_OFFSET * (vector3.y - vector3.x);

            return new Vector3(isometricX, isometricY);
        }
    }
}