using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsJumpingConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetIsJumpingData(out var isJumping) && isJumping.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsNotJumpingConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetIsJumpingData(out var isJumping) || !isJumping.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsJumpHangingConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetIsJumpHangingData(out var isJumpHanging) && isJumpHanging.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsNotJumpHangingConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetIsJumpHangingData(out var isJumpHanging) || !isJumpHanging.CurrentValue;
        }
    }
}