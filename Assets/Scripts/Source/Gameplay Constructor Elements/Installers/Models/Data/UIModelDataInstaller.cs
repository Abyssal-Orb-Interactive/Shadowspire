using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public class UIModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _currentlyOpenedWindow = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _inventoryWindowEntity = new();
        [SerializeField] private AtomicReactiveProperty<GameObject> _inventoryWindow = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _inputHandler = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddCurrentlyOpenWindowData(_currentlyOpenedWindow);
            entity.TryAddInventoryWindowData(_inventoryWindow);
            entity.TryAddInventoryWindowEntityData(_inventoryWindowEntity);
            entity.TryAddInputHandlerData(_inputHandler);
        }
    }
}