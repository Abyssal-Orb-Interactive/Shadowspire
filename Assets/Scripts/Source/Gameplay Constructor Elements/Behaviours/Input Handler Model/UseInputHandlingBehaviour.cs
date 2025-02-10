using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplayConstructorElements.Behaviours.InputHandlerModel
{
    [Serializable]
    public sealed class UseInputHandlingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;
        private IAtomicAction _useInputAction = null;

        #endregion
        
        #region Constructors
        
        public UseInputHandlingBehaviour() {}
        public UseInputHandlingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;

            _entity.TryGetUseInputActionData(out var useInputAction);
            _useInputAction = useInputAction;
            
            OnInit();
        }

        public void OnInit() {}
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }
        
        public void OnAwake()
        {
            _inputActions.CurrentValue.BaseMap.Use.performed += OnUseInput;
        }
        
        private void OnUseInput(InputAction.CallbackContext context)
        {
            _useInputAction.Invoke();
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
            _inputActions.CurrentValue.BaseMap.Use.performed -= OnUseInput;
        }
        
        #endregion
    }
}