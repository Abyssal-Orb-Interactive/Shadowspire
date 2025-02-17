using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsInCooldownConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetIsInAttackCooldownData(out var isInCooldown) && isInCooldown.CurrentValue;
        }
    }
    
    
    [Serializable]
    public sealed class IsNotInCooldownConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetIsInAttackCooldownData(out var isInCooldown) || !isInCooldown.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsStunnedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetIsStunnedData(out var isStunned) && isStunned.CurrentValue;
        }
    }
    
    
    [Serializable]
    public sealed class IsNotStunnedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetIsStunnedData(out var isStunned) || !isStunned.CurrentValue;
        }
    }
}