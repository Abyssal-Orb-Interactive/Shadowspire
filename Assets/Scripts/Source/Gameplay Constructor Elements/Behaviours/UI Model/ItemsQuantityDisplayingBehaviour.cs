using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using R3;
using TMPro;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.UI_Model
{
    [Serializable]
    public sealed class ItemsQuantityDisplayingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Varibles

        private AtomicReactiveProperty<IEntity> _displayingEntity = null;
        private IReadonlyAtomicReactiveProperty<IEntity> _inventoryHolder = null;
        private ObservableDictionary<string, AtomicReactiveProperty<int>> _inventory  = null;
        private ReactiveLibraryFacade.IObservable<int> _quantity  = null;
        private IAtomicValue<TMP_Text> _quantityUI  = null;
        private IAtomicValue<GameObject> _quantityGameObject = null;

        #endregion

        #region Subscriptions

        private IDisposable _quantitySubscription = null;
        private IDisposable _entitySubscription = null;
        private IDisposable _inventoryHolderSubscription = null;
        private IDisposable _inventorySubscription = null;

        #endregion

        #region Constructors

        public ItemsQuantityDisplayingBehaviour() {}
        public ItemsQuantityDisplayingBehaviour(IEntity entity) : base(entity) {}

        #endregion
        
        public void Init()
        {
            _entity.TryGetDisplayingEntityData(out var displayingEntity);
            _displayingEntity = displayingEntity;

            _entity.TryGetUIQuantityData(out var quantityUI);
            _quantityUI = quantityUI;
            
            _entity.TryGetInventoryHolderData(out var inventoryHolder);
            _inventoryHolder = inventoryHolder;
            
            _entity.TryGetQuantityGameObjectData(out var quantityGameObject);
            _quantityGameObject = quantityGameObject;
            
            OnInit();
        }

        public void OnInit()
        {
        }
        
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _inventoryHolderSubscription = _inventoryHolder.Subscribe(OnInventoryHolderChanged);
            _entitySubscription = _displayingEntity.Subscribe(OnDisplayingEntityChanged);
        }

        private void OnInventoryHolderChanged(IEntity newInventoryHolder)
        {
            _quantitySubscription?.Dispose();
            _quantitySubscription = null;
            _entitySubscription?.Dispose();
            _entitySubscription = null;
            _inventorySubscription?.Dispose();
            _inventorySubscription = null;
            
            _quantityGameObject.CurrentValue.SetActive(false);
            _quantityUI.CurrentValue.text = 0.ToString();
            
            
            if (newInventoryHolder == null) return;
            
            newInventoryHolder.TryGetInventoryData(out var inventory);
            
            _inventory = inventory;

            if(_inventory == null) return;
            
            _inventorySubscription = _inventory.ObserveAdd().Subscribe(OnItemAdded);
            
            if (!_displayingEntity.CurrentValue.TryGetNameData(out var name) || 
                !_inventory.ContainsKey(name.CurrentValue))
            {
                return;
            }
            
            _quantityGameObject.CurrentValue.SetActive(true);
            _quantity = _inventory[name.CurrentValue];
            _quantitySubscription = _quantity.Subscribe(OnQuantityChanged);
        }

        private void OnDisplayingEntityChanged(IEntity newEntity)
        {
            _quantitySubscription?.Dispose();
            _quantitySubscription = null;
            _inventorySubscription?.Dispose();
            _inventorySubscription = null;
            
            _quantityGameObject.CurrentValue.SetActive(false);
            _quantityUI.CurrentValue.text = 0.ToString();
            
            if (newEntity == null || _inventory == null) return;

            _inventorySubscription = _inventory.ObserveAdd().Subscribe(OnItemAdded);

            if (!newEntity.TryGetNameData(out var name) || !_inventory.TryGetValue(name.CurrentValue, out var quantity))
            {
                _displayingEntity.Value = null;
                return;
            }

            _quantityGameObject.CurrentValue.SetActive(true);
            _quantity = quantity;
            _quantitySubscription = _quantity.Subscribe(OnQuantityChanged);
        }
        

        private void OnItemAdded(CollectionAddEvent<KeyValuePair<string, AtomicReactiveProperty<int>>> newPair)
        {
            if (!_displayingEntity.CurrentValue.TryGetNameData(out var name)) return;
            if (newPair.Value.Key != name.CurrentValue) return;
            
            _quantitySubscription?.Dispose();
            _quantitySubscription = null;
            
            _quantityGameObject.CurrentValue.SetActive(true);
            _quantity = newPair.Value.Value;
            _quantitySubscription = _quantity.Subscribe(OnQuantityChanged);
        }
        
        private void OnQuantityChanged(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                _quantityUI.CurrentValue.text = 0.ToString();
                _quantityGameObject.CurrentValue.SetActive(false);
                _displayingEntity.Value = null;
                return;
            }
            
            _quantityUI.CurrentValue.text = newQuantity.ToString();
        }

        public void Sleep()
        {
            OnSleep();
        }

        public void OnSleep()
        {
            Dispose();
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _quantitySubscription?.Dispose();
            _quantitySubscription = null;
            _entitySubscription?.Dispose();
            _entitySubscription = null;
            _inventoryHolderSubscription?.Dispose();
            _inventoryHolderSubscription = null;
            _inventorySubscription?.Dispose();
            _inventorySubscription = null;
        }
    }
}