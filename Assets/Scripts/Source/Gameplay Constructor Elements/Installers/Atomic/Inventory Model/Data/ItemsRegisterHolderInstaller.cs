using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.InventoryModel.Data
{
    [Serializable]
    public sealed class ItemsRegisterHolderInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _itemsRegisterHolder = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddItemsRegisterHolderData(_itemsRegisterHolder);
        }
    }
}