using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.HealthModel.Data
{
    [Serializable]
    public sealed class MaxHealthInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _maxHealth = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddMaxHealthData(_maxHealth);
        }
    }
}