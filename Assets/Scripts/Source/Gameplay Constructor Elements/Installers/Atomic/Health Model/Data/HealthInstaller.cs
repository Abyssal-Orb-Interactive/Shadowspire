using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.HealthModel.Data
{
    [Serializable]
    public class HealthInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _health = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddHealthData(_health);
        }
    }
}