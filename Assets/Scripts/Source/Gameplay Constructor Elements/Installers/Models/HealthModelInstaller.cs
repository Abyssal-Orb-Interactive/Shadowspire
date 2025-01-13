using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class HealthModelInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _maxHealth = new();
        [SerializeField] private AtomicReactiveProperty<float> _health = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddMaxHealthData(_maxHealth);
            entity.TryAddHealthData(_health);
        }
    }
}