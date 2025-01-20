using System;
using System.Collections.Generic;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.InventoryModel.Data
{
    [Serializable]
    public sealed class ItemsRegisterInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private Dictionary<string, IEntity> _items = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddItemsRegisterData(_items);
        }
    }
}