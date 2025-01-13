using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class ComponentExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, float3 targetPosition)
        {
            component.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<float3> targetPosition)
        {
            component.transform.position = targetPosition.CurrentValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, float2 targetPosition)
        {
            component.transform.position = new float3(targetPosition.x, targetPosition.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<float2> targetPosition)
        {
            var pos = targetPosition.CurrentValue;
            component.transform.position = new float3(pos.x, pos.y, 0f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, Vector3 targetPosition)
        {
            component.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<Vector3> targetPosition)
        {
            component.transform.position = targetPosition.CurrentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, Vector2 targetPosition)
        {
            component.transform.position = targetPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<Vector2> targetPosition)
        {
            component.transform.position = targetPosition.CurrentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, Transform target)
        {
            component.transform.position = target.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<Transform> target)
        {
            component.transform.position = target.CurrentValue.position;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, GameObject target)
        {
            component.transform.position = target.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<GameObject> target)
        {
            component.transform.position = target.CurrentValue.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, Component target)
        {
            component.transform.position = target.transform.position;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static void TeleportTo(this Component component, IAtomicValue<Component> target)
        {
            component.transform.position = target.CurrentValue.transform.position;
        }
    }
}