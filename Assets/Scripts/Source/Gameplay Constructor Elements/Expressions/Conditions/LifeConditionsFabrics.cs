using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

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

    [Serializable]
    public sealed class IsFullHealedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetHealthPercentData(out var healthPercent) && Mathf.Approximately(healthPercent.CurrentValue, 1f);
        }
    }
    
    [Serializable]
    public sealed class IsNotFullHealedConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetHealthPercentData(out var healthPercent) || !Mathf.Approximately(healthPercent.CurrentValue, 1f);
        }
    }
}