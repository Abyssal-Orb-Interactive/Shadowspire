using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsAliveConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetHealthPercentData(out var healthPercent) && healthPercent.CurrentValue > 0f;
        }
    }
    
    
    [Serializable]
    public sealed class IsDeadConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetHealthPercentData(out var healthPercent) || healthPercent.CurrentValue <= 0f;
        }
    }
}