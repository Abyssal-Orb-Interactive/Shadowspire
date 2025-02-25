using System;
using System.Collections.Generic;
using System.Linq;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFramework.Enitity.World;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using R3;
using UnityEngine;
using DisposableBuilder = ReactiveLibraryFacade.DataStructures.DisposableBuilder;

namespace GameplayConstructorElements.Behaviours.UIModel
{
    [Serializable]
    public sealed class InventoryDisplayingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private Dictionary<string, IEntity> _usedInventorySlots = new();
        
        #region Cache Variables

        private IAtomicValue<IEntity> _itemsRegisterHolder = null;
        private Dictionary<string, IEntity> _itemsRegister = null;  
        private IReadonlyAtomicReactiveProperty<IEntity> _displayingEntity = null;
        private ObservableDictionary<string, AtomicReactiveProperty<int>> _inventory  = null;
        private IAtomicValue<List<IEntity>> _inventorySlots = null;
        private IAtomicValue<GameObject> _inventorySlotPrefab = null;
        private IAtomicValue<GameObject> _inventoryScrollViewContentHolder = null;
        private IAtomicValue<IWorld> _world = null;
        private IAtomicValue<GameObject> _inventoryWindow = null;
        
        #endregion
        
        #region Subscriptions
        
        private IDisposable _inventorySubscription = null;
        private IDisposable _entitySubscription = null;
        
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
            
            _entity.TryGetWorldData(out var world);
            _world = world;
            
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
            _entitySubscription = _displayingEntity.Subscribe(OnDisplayingEntityChange);
            
            var count = _usedInventorySlots.Count;
            var slots = _usedInventorySlots.Values.ToList();
            for (var i = 0; i < count; i++)
            {
                slots[i].IsActive.Value = true;
            }
        }
        
        private void OnDisplayingEntityChange(IEntity newEntity)
        {
            _inventorySubscription?.Dispose();
            _inventorySubscription = null;
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
                if (!slot.TryGetInventoryHolderData(out var inventoryHolder)) continue;
                
                inventoryHolder.Value = _displayingEntity.CurrentValue;
                displayingEntity.Value = item;
                _usedInventorySlots[id] = slot;
                index++;
            }
            
            _inventoryWindow.CurrentValue.SetActive(true);

            var inventorySubscriptionBuilder = new DisposableBuilder();
            inventorySubscriptionBuilder.Add(_inventory.ObserveRemove().Subscribe(OnItemRemoved));
            inventorySubscriptionBuilder.Add(_inventory.ObserveAdd().Subscribe(OnItemAdded));

            _inventorySubscription = inventorySubscriptionBuilder.Build();
        }
        
        private void OnItemRemoved(CollectionRemoveEvent<KeyValuePair<string, AtomicReactiveProperty<int>>> tokenOfRemovable)
        {
            var itemID = tokenOfRemovable.Value.Key;
            
            _usedInventorySlots[itemID].Dispose();
            _usedInventorySlots.Remove(itemID);
        }
        
        private void OnItemAdded(CollectionAddEvent<KeyValuePair<string, AtomicReactiveProperty<int>>> tokenOfRemovable)
        {
            var itemID = tokenOfRemovable.Value.Key;
            
            var id = _world.CreateEntity(_inventorySlotPrefab, _inventoryScrollViewContentHolder);
            if (_world.CurrentValue.TryGetEntityWithID(id, out var newSlot))
            {
                _inventorySlots.CurrentValue.Add(newSlot);
            }
            
            var item = _itemsRegister[itemID];
            
            if (!newSlot.TryGetDisplayingEntityData(out var displayingEntity)) return;
            if (!newSlot.TryGetInventoryHolderData(out var inventoryHolder)) return;
            
            inventoryHolder.Value = _displayingEntity.CurrentValue;
            displayingEntity.Value = item;
            _usedInventorySlots[itemID] = newSlot;
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
            _entitySubscription?.Dispose();
            _entitySubscription = null;
            _inventorySubscription?.Dispose();
            _inventorySubscription = null;
        }
        
        #endregion
    }
}