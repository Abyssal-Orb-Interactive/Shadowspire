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
    public sealed class InteractionInputHandlingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;

        private AtomicEvent _interactionInputAction = null;
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;

        public InteractionInputHandlingBehaviour()
        {
            _entity = new Entity();
        }
        
        public InteractionInputHandlingBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;

            _entity.TryGetInteractionInputActionData(out _interactionInputAction);
            
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
            _inputActions.CurrentValue.BaseMap.Interaction.performed += OnInteractionInput;
        }

        private void OnInteractionInput(InputAction.CallbackContext context)
        {
            _interactionInputAction.Invoke();
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
        }
    }
}