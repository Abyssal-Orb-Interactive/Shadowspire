using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Int3Extensions
    {
        private const float X_ISOMETRIC_OFFSET = 0.5f;
        private const float Y_ISOMETRIC_OFFSET = 0.25f;
        private const float CARTESIAN_OFFSET = 2f;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ToVector3Int(this int3 int3)
        {
            return new Vector3Int(int3.x, int3.y, int3.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ToVector2Int(this int3 int3)
        {
            return new Vector2Int(int3.x, int3.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToIsometric(this int3 int3)
        {
            var isometricX = X_ISOMETRIC_OFFSET * (int3.x + int3.y);
            var isometricY = Y_ISOMETRIC_OFFSET * (int3.y - int3.x);
            
            return new float3(isometricX, isometricY, int3.z);
        }
    }
}