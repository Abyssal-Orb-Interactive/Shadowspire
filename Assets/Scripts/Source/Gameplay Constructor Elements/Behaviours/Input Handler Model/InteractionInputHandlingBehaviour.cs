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
    public sealed class InteractionInputHandlingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicAction _interactionInputAction = null;
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;

        #endregion

        #region Constructors
        
        public InteractionInputHandlingBehaviour() {}
        public InteractionInputHandlingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;

            _entity.TryGetInteractionInputActionData(out var interactionInputAction);
            _interactionInputAction = interactionInputAction;
            
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
            _inputActions.CurrentValue.BaseMap.Interaction.performed -= OnInteractionInput;
        }
        
        #endregion
    }
}