using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.Attack_Model.Data
{
    [Serializable]
    public sealed class AimInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _aim = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddAimData(_aim);
        }
    }
}