using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFramework.Enitity.World;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.UIModel
{
    [Serializable]
    public sealed class InventoryDisplayingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private Dictionary<string, IEntity> _usedInventorySlots = new();
        
        #region Cache Variables

        private IAtomicValue<IEntity> _itemsRegisterHolder = null;
        private Dictionary<string, IEntity> _itemsRegister = null;  
        private ReactiveLibraryFacade.IObservable<IEntity> _displayingEntity = null;
        private ObservableDictionary<string, AtomicReactiveProperty<int>> _inventory  = null;
        private IAtomicValue<List<IEntity>> _inventorySlots = null;
        private IAtomicValue<GameObject> _inventorySlotPrefab = null;
        private IAtomicValue<GameObject> _inventoryScrollViewContentHolder = null;
        private IAtomicValue<IWorld> _world = null;
        private IAtomicValue<GameObject> _inventoryWindow = null;
        
        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion

        #region Constuctors

        public InventoryDisplayingBehaviour()
        {
            _usedInventorySlots = new Dictionary<string, IEntity>();
        }

        public InventoryDisplayingBehaviour(IEntity entity) : base(entity)
        {
            _usedInventorySlots = new Dictionary<string, IEntity>();
        }

        #endregion

        #region Life Cycle Methods

        public void Init()
        {
            _entity.TryGetItemsRegisterHolderData(out var itemsRegisterHolder);
            _itemsRegisterHolder = itemsRegisterHolder;
            
            _entity.TryGetDisplayingEntityData(out var displayingEntity);
            _displayingEntity = displayingEntity;

            _entity.TryGetInventorySlotsData(out var inventorySlots);
            _inventorySlots = inventorySlots;
            
            _entity.TryGetInventorySlotPrefabData(out var inventorySlotPrefab);
            _inventorySlotPrefab = inventorySlotPrefab;

            _entity.TryGetInventoryScrollViewContentHolderData(out var inventoryScrollViewContentHolder);
            _inventoryScrollViewContentHolder = inventoryScrollViewContentHolder;

            _entity.TryGetInventoryWindowData(out var inventoryWindow);
            _inventoryWindow = inventoryWindow;
            
            OnInit();
        }

        public void OnInit()
        {
            _itemsRegisterHolder.CurrentValue.TryGetItemsRegisterData(out var itemsRegister);
            _itemsRegister = itemsRegister;
            
            
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _displayingEntity.Subscribe(OnDisplayingEntityChange);
        }

        private void OnDisplayingEntityChange(IEntity newEntity)
        {
            _usedInventorySlots.Clear();

            if (newEntity == null)
            {
                _inventoryWindow.CurrentValue.SetActive(false);
                return;
            }

            if (!newEntity.TryGetInventoryData(out var inventory))
            {
                _inventoryWindow.CurrentValue.SetActive(false);
                return;
            }
            
            _inventory = inventory;

            var quantityDifference = _inventorySlots.CurrentValue.Count - _inventory.Count;

            if (quantityDifference < 0)
            {
                for (var i = 0; i < -quantityDifference; i++)
                {
                    var id = _world.CreateEntity(_inventorySlotPrefab, _inventoryScrollViewContentHolder);
                    if (_world.CurrentValue.TryGetEntityWithID(id, out var newSlot))
                    {
                        _inventorySlots.CurrentValue.Add(newSlot);
                    }
                }
            }

            var index = 0;
            foreach (var (id, _) in _inventory)
            {
                var item = _itemsRegister[id];
                var slot = _inventorySlots.CurrentValue[index];

                if (!slot.TryGetDisplayingEntityData(out var displayingEntity)) continue;
                
                displayingEntity.Value = item;
                _usedInventorySlots[id] = slot;
                index++;
            }
        }

        public void Sleep()
        {
            OnSleep();
        }

        public void OnSleep()
        {
            Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}