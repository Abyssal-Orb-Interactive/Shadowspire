using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.UnityExtensions;
using TimeFramework.Core;
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CalculateTargetPositionWithDistance(in IAtomicValue<Transform> targetPosition, in IAtomicValue<float3> distance)
        {
            return CalculateTargetPositionWithDistance(targetPosition, distance.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CalculateTargetPositionWithDistance(in IAtomicValue<Transform> targetPosition, in float3 distance)
        {
            return CalculateTargetPositionWithDistance(targetPosition.CurrentValue, distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CalculateTargetPositionWithDistance(in Transform targetPosition, in float3 distance)
        {
            return CalculateTargetPositionWithDistance(targetPosition.position, distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CalculateTargetPositionWithDistance(in Vector3 targetPosition, in float3 distance)
        {
            return CalculateTargetPositionWithDistance(targetPosition.ToFloat3(), distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CalculateTargetPositionWithDistance(in float3 targetPosition, in float3 distance)
        {
            return targetPosition - distance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedStepByDeltaTime(in IAtomicValue<float> speed, in TimeInvoker timeInvoker)
        {
            return CalculateSpeedStepByDeltaTime(speed.CurrentValue, timeInvoker.DeltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedStepByDeltaTime(in IAtomicValue<float> speed, in float deltaTime)
        {
            return CalculateSpeedStepByDeltaTime(speed.CurrentValue, deltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedStepByDeltaTime(in float speed, in float deltaTime)
        {
            if(deltaTime < 0f) return 0f;
            
            return speed * deltaTime;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedAfterAccelerationWithDeltaTime(in float speed, in IAtomicValue<float> acceleration, in TimeInvoker timeInvoker)
        {
            return CalculateSpeedAfterAccelerationWithDeltaTime(speed, acceleration, timeInvoker.FixedDeltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedAfterAccelerationWithDeltaTime(in float speed, in IAtomicValue<float> acceleration, in float deltaTime)
        {
            return CalculateSpeedAfterAccelerationWithDeltaTime(speed, acceleration.CurrentValue, deltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateSpeedAfterAccelerationWithDeltaTime(in float speed, in float acceleration, in float deltaTime)
        {
            return speed + acceleration * deltaTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateJumpVelocity(in float gravity, in float height)
        {
            if(gravity <= 0f || height <= 0f) return 0f;
            
            return math.sqrt(2f * gravity * height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateIncreasedYVelocityWithModifer(in float2 velocity, in float ySpeedupModifier)
        {
            return CalculateIncreasedYVelocityWithModifer(velocity.x, velocity.y, ySpeedupModifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateIncreasedYVelocityWithModifer(in float2 velocity, in IAtomicValue<float> ySpeedupModifier)
        {
            return CalculateIncreasedYVelocityWithModifer(velocity.x, velocity.y, ySpeedupModifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateIncreasedYVelocityWithModifer(in float xComponent, in float yComponent, in IAtomicValue<float> ySpeedupModifier)
        {
            return CalculateIncreasedYVelocityWithModifer(xComponent, yComponent, ySpeedupModifier.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateIncreasedYVelocityWithModifer(in float xComponent, in float yComponent, in float ySpeedupModifier)
        {
            return ySpeedupModifier <= 0f ? new float2(xComponent, yComponent) : new float2(xComponent, yComponent * (1f + ySpeedupModifier));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateDecreasedYVelocityWithModifer(in float2 velocity, in float ySlowModifier)
        {
            return CalculateDecreasedYVelocityWithModifer(velocity.x, velocity.y, ySlowModifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateDecreasedYVelocityWithModifer(in float2 velocity, in IAtomicValue<float> ySlowModifier)
        {
            return CalculateDecreasedYVelocityWithModifer(velocity.x, velocity.y, ySlowModifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateDecreasedYVelocityWithModifer(in float xComponent, in float yComponent, in IAtomicValue<float> ySlowModifier)
        {
            return CalculateDecreasedYVelocityWithModifer(xComponent, yComponent, ySlowModifier.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 CalculateDecreasedYVelocityWithModifer(in float xComponent, in float yComponent, in float ySlowModifier)
        {
            return ySlowModifier <= 0f ? new float2(xComponent, yComponent) : new float2(xComponent, yComponent * (1f - ySlowModifier));
        }
    }
}