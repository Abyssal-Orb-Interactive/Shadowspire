using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.InventoryModel.Data
{
    [Serializable]
    public sealed class UserInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _user = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddUserData(_user);
        }
    }
}