using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.DamageModel.Data
{
    [Serializable]
    public sealed class DamageInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _damage = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddDamageData(_damage);
        }
    }
}