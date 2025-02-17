using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class HealthBarModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Image> _healthbar = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _displayingEntity = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddHealthBarData(_healthbar);
            entity.TryAddDisplayingEntityData(_displayingEntity);
        }
    }
}