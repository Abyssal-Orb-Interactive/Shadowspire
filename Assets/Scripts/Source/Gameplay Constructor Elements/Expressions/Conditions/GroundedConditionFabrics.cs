using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsGroundedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetIsGroundedData(out var isGrounded) && isGrounded.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsNotGroundedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetIsGroundedData(out var isGrounded) || !isGrounded.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsGroundedOrInCoyoteTimeConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () =>
                (entity.TryGetIsGroundedData(out var isGrounded) && isGrounded.CurrentValue) ||
                (entity.TryGetIsInCoyoteTimeData(out var isInCoyoteTime) && isInCoyoteTime.CurrentValue);
        }
    }
    
    [Serializable]
    public sealed class IsNotGroundedAndNotInCoyoteTimeConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () =>
                (!entity.TryGetIsGroundedData(out var isGrounded) || !isGrounded.CurrentValue) &&
                (!entity.TryGetIsInCoyoteTimeData(out var isInCoyoteTime) || !isInCoyoteTime.CurrentValue);
        }
    }
}