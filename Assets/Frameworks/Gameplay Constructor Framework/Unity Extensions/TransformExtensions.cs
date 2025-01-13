using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class TransformExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, float3 targetPosition)
        {
            transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<float3> targetPosition)
        {
            transform.position = targetPosition.CurrentValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, float2 targetPosition)
        {
            transform.position = new float3(targetPosition.x, targetPosition.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<float2> targetPosition)
        {
            var pos = targetPosition.CurrentValue;
            transform.position = new float3(pos.x, pos.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<Vector3> targetPosition)
        {
            transform.position = targetPosition.CurrentValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, Vector2 targetPosition)
        {
            transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<Vector2> targetPosition)
        {
            transform.position = targetPosition.CurrentValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, Transform target)
        {
            transform.position = target.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<Transform> target)
        {
            transform.position = target.CurrentValue.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, GameObject target)
        {
            transform.position = target.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<GameObject> target)
        {
            transform.position = target.CurrentValue.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, Component target)
        {
            transform.position = target.transform.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Transform transform, IAtomicValue<Component> target)
        {
            transform.position = target.CurrentValue.transform.position;
        }
    }
}