using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class GameObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, float3 targetPosition)
        {
            gameObject.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<float3> targetPosition)
        {
            gameObject.transform.position = targetPosition.CurrentValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, float2 targetPosition)
        {
            gameObject.transform.position = new float3(targetPosition.x, targetPosition.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<float2> targetPosition)
        {
            var pos = targetPosition.CurrentValue;
            gameObject.transform.position = new float3(pos.x, pos.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, Vector3 targetPosition)
        {
            gameObject.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<Vector3> targetPosition)
        {
            gameObject.transform.position = targetPosition.CurrentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, Vector2 targetPosition)
        {
            gameObject.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<Vector2> targetPosition)
        {
            gameObject.transform.position = targetPosition.CurrentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, Transform target)
        {
            gameObject.transform.position = target.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<Transform> target)
        {
            gameObject.transform.position = target.CurrentValue.position;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, GameObject target)
        {
            gameObject.transform.position = target.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<GameObject> target)
        {
            gameObject.transform.position = target.CurrentValue.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, Component target)
        {
            gameObject.transform.position = target.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this GameObject gameObject, IAtomicValue<Component> target)
        {
            gameObject.transform.position = target.CurrentValue.transform.position;
        }
    }
}