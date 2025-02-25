using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Enitity.World;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class InventoryModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _displayingEntity = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _inventoryWindow = new();
        [SerializeField] private AtomicReactiveProperty<List<IEntity>> _slots = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _inventorySlotPrefab = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _inventoryScrollViewContentHolder = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _itemsRegisterHolder = new();
        [SerializeField] private AtomicReactiveProperty<IWorld> _world = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddDisplayingEntityData(_displayingEntity);
            entity.TryAddInventoryWindowData(_inventoryWindow);
            entity.TryAddInventorySlotsData(_slots);
            entity.TryAddInventorySlotPrefabData(_inventorySlotPrefab);
            entity.TryAddInventoryScrollViewContentHolderData(_inventoryScrollViewContentHolder);
            entity.TryAddItemsRegisterHolderData(_itemsRegisterHolder);
            entity.TryAddWorldData(_world);
        }
    }
}