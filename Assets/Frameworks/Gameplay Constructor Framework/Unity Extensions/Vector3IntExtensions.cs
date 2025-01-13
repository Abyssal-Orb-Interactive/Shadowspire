using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Vector3IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ToInt3(this Vector3Int vector3Int)
        {
            return new int3(vector3Int.x, vector3Int.y, vector3Int.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ToInt2(this Vector3Int vector3Int)
        {
            return new int2(vector3Int.x, vector3Int.y);
        }
    }
}