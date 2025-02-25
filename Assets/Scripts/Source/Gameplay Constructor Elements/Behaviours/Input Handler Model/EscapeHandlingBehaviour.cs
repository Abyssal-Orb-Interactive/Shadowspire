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
    public sealed class EscapeHandlingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicAction _escapeAction = null;
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;

        #endregion

        #region Constructors

        public EscapeHandlingBehaviour() {}
        public EscapeHandlingBehaviour(IEntity entity) : base(entity) {}

        #endregion

        #region Life Cycle Methods

        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;
            
            _entity.TryGetEscapeInputActionData(out var escapeAction);
            _escapeAction = escapeAction;
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
            _inputActions.CurrentValue.BaseMap.EscapeAction.performed += OnEscapeAction;
        }

        private void OnEscapeAction(InputAction.CallbackContext callback)
        {
            _escapeAction.Invoke();
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
            _inputActions.CurrentValue.BaseMap.EscapeAction.performed -= OnEscapeAction;
        }
        
        #endregion
    }
}