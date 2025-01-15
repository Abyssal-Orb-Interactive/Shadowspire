using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class HealthModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _maxHealth = new();
        [SerializeField] private AtomicReactiveProperty<float> _health = new();
        [SerializeReference] private IEntityConditionFabric[] _canTakeDamageConditionFabrics = Array.Empty<IEntityConditionFabric>();
        [SerializeField] private AtomicReactiveProperty<bool> _invincibility = new();
        [SerializeField] private AtomicReactiveProperty<float> _invincibilitySecondsDuration = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddMaxHealthData(_maxHealth);
            entity.TryAddHealthData(_health);
            entity.TryAddInvincibilityData(_invincibility);
            entity.TryAddInvincibilitySecondsDurationData(_invincibilitySecondsDuration);
            
            var canTakeDamageCondition = new AtomicBoolMultiplication();
            canTakeDamageCondition.AppendBy(_canTakeDamageConditionFabrics, entity);
            entity.TryAddCanTakeDamageData(canTakeDamageCondition);
        }
    }
}