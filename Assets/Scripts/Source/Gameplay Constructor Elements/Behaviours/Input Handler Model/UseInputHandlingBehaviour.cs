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
    public sealed class UseInputHandlingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;
        private IAtomicAction _useInputAction = null;

        public UseInputHandlingBehaviour()
        {
            _entity = new Entity();
        }

        public UseInputHandlingBehaviour(IEntity entity)
        {
            _entity = entity;
        }


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
    }
}