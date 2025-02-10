using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class DamageModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _damage = new();
        [SerializeField] private AtomicReactiveProperty<DamageType> _damageType = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddDamageData(_damage);
            entity.TryAddDamageTypeData(_damageType);
        }
    }
}