using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.UnityExtensions;
using Unity.Mathematics;
using UnityEngine;

namespace UseCases
{
    public static class MovementCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateOffset(in float2 direction, in float speed, in float deltaTime)
        {
            if (speed <= 0f) return direction * deltaTime;
            
            return direction * (speed * deltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateOffset(in float2 direction, in IAtomicValue<float> speed, in float deltaTime)
        {
            return CalculateOffset(direction, speed.CurrentValue, deltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateTargetPosition(in Rigidbody2D rigidbody2D, in float2 offset)
        {
            return rigidbody2D.position.ToFloat2() + offset;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateTargetPosition(in IAtomicValue<Rigidbody2D> rigidbody2D, in float2 offset)
        {
            return CalculateTargetPosition(rigidbody2D.CurrentValue, offset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateTargetPosition(in IAtomicValue<Rigidbody2D> rigidbody2D, in float2 direction, in IAtomicValue<float> speed, in float deltaTime)
        {
            var offset = CalculateOffset(direction, speed, deltaTime);
            return CalculateTargetPosition(rigidbody2D, offset);
        }
    }
}