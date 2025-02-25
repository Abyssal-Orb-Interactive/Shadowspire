using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace Source.Gameplay_Constructor_Elements.Behaviours.UI_Model
{
    [Serializable]
    public sealed class InventoryWindowTogglingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicValue<IEntity> _inputHandler = null;
        private ReactiveLibraryFacade.IObservable _inventoryInputAction = null;
        private IAtomicVariable<IEntity> _window = null;
        private IAtomicValue<IEntity> _inventoryWindowEntity = null;
        private IAtomicValue<GameObject> _inventoryWindow = null;

        #endregion
        
        #region Subscriptions

        private IDisposable _subscription = null;
        
        #endregion

        #region Constructors

        public InventoryWindowTogglingBehaviour() {}
        public InventoryWindowTogglingBehaviour(IEntity entity) : base(entity) {}

        #endregion

        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;

            _entity.TryGetCurrentlyOpenWindowData(out var window);
            _window = window;
            
            _entity.TryGetInventoryWindowEntityData(out var inventoryWindowEntity);
            _inventoryWindowEntity = inventoryWindowEntity;
            
            _entity.TryGetInventoryWindowData(out var inventoryWindow);
            _inventoryWindow = inventoryWindow;
            
            OnInit();
        }

        public void OnInit()
        {
            _inputHandler.CurrentValue.TryGetInventoryInputActionData(out var inventoryInputAction);
            _inventoryInputAction = inventoryInputAction;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _inventoryInputAction.Subscribe(OnInventoryInputAction);
        }

        private void OnInventoryInputAction()
        {
            var window = _window.CurrentValue;

            if (window != null && window.IsActive.CurrentValue)
            {
                window.IsActive.Value = false;
                
                if (window.ID == _inventoryWindowEntity.CurrentValue.ID)
                {
                    _window.Value = null;
                    return;
                }
            }
            
            
            _window.Value = _inventoryWindowEntity.CurrentValue;
            _inventoryWindow.CurrentValue.SetActive(true);
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
            _subscription = null;
        }
    }
}