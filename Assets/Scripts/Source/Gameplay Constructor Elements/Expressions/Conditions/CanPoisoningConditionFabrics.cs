using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsPoisonImmuneConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetDamageModifiersData(out var modifiers) &&
                         modifiers.TryGetValue((int)DamageType.Poison, out var poisonDamageModifier) &&
                         Mathf.Approximately(poisonDamageModifier, 1f);
        }
    }
    
    
    [Serializable]
    public sealed class IsNotPoisonImmuneConditionFabric : IEntityConditionFabric
    {
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetDamageModifiersData(out var modifiers) ||
                         !modifiers.TryGetValue((int)DamageType.Poison, out var poisonDamageModifier) ||
                         !Mathf.Approximately(poisonDamageModifier, 1f);
        }
    }
}