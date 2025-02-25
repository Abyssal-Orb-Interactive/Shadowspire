using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using UnityEngine.InputSystem;

namespace GameplayConstructorElements.Behaviours.InputHandlerModel
{
    [Serializable]
    public sealed class InventoryActionHandlingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicAction _inventoryAction = null;
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;

        #endregion

        #region Constructors

        public InventoryActionHandlingBehaviour() {}
        public InventoryActionHandlingBehaviour(IEntity entity) : base(entity) {}

        #endregion

        #region Life Cycle Methods

        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;
            
            _entity.TryGetInventoryInputActionData(out var inventoryAction);
            _inventoryAction = inventoryAction;
        }

        public void OnInit()
        {
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
            _inputActions.CurrentValue.BaseMap.InventoryAction.performed += OnInventoryAction;
        }

        private void OnInventoryAction(InputAction.CallbackContext callback)
        {
            _inventoryAction.Invoke();
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
            _inputActions.CurrentValue.BaseMap.InventoryAction.performed -= OnInventoryAction;
        }
        
        #endregion
    }
}