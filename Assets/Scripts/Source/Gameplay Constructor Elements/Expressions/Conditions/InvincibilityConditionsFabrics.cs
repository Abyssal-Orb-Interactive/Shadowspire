using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsInvincibleСonditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetInvincibilityData(out var invincibility) && invincibility.CurrentValue;
        }
    }
    
    [Serializable]
    public sealed class IsNotInvincibleСonditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetInvincibilityData(out var invincibility) || !invincibility.CurrentValue;
        }
    }
}