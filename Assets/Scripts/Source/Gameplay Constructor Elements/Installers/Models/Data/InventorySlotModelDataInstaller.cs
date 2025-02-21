using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class InventorySlotModelDataInstaller : IEntityAtomicElementInstaller
    {
        
        [Header("Icon")]
        [SerializeField] private AtomicReactiveProperty<IEntity> _displayingEntity = new();
        [SerializeField] private AtomicReactiveProperty<Image> _icon = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _iconGameObbject = new();
        
        [Header("Quantity")]
        [SerializeField] private AtomicReactiveProperty<TMP_Text> _quantityUI = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _quantityGameObject = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _inventoryHolder = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddDisplayingEntityData(_displayingEntity);
            entity.TryAddIconData(_icon);
            entity.TryAddIconGameObjectData(_iconGameObbject);
            entity.TryAddUIQuantityData(_quantityUI);
            entity.TryAddQuantityGameObjectData(_quantityGameObject);
            entity.TryAddInventoryHolderData(_inventoryHolder);
        }
    }
}