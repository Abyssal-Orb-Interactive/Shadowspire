using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameplayConstructorElements.Behaviours.InventoryModel
{
    [Serializable]
    public sealed class ItemsDroppingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IAtomicValue<IEntity> _itemsRegisterHolder = null;
        private ObservableDictionary<string, int> _inventory = null;
        private Dictionary<string, IEntity> _itemsRegister = null;
        private ReactiveLibraryFacade.IObservable<float> _health = null;
        
        private IDisposable _subscription = null;

        public ItemsDroppingBehaviour()
        {
            _entity = new Entity();
        }

        public ItemsDroppingBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetItemsRegisterHolderData(out var itemsRegisterHolder);
            _itemsRegisterHolder = itemsRegisterHolder;
            
            _entity.TryGetInventoryData(out _inventory);
            
            _entity.TryGetHealthData(out var health);
            _health = health;
            
            OnInit();
        }
        
        public void OnInit()
        {
            _itemsRegisterHolder.CurrentValue.TryGetItemsRegisterData(out _itemsRegister);
        }
        
        public void Awake()
        {
            OnAwake();
        }
        
        public void OnAwake()
        {
            _subscription = _health.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float newHealth)
        {
            if(newHealth > 0f) return;
            
            foreach (var (id, quantity) in _inventory)
            {
                if (!_itemsRegister.TryGetValue(id, out var itemPrefab)) continue;
                if (itemPrefab is not Component entityComponent) continue;
                
                for (var i = 0; i < quantity; i++)
                {
                    Object.Instantiate(entityComponent.gameObject, Vector3.zero, Quaternion.identity);
                }
            }
            
            _inventory.Clear();
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
            _subscription?.Dispose();
        }
    }
}