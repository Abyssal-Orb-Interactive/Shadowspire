using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.UnityExtensions;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplayConstructorElements.Behaviours.InputHandlerModel
{
    [Serializable]
    public sealed class MovementInputHandlingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IReadonlyAtomicReactiveProperty<PlayerActions> _inputActions = null;
        private AtomicEvent<float2> _movementInputAction = null;
        
        private PlayerActions _cache = null;
        private IDisposable _subscribe = null;

        public MovementInputHandlingBehaviour()
        {
            _entity = new Entity();
        }

        public MovementInputHandlingBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;

            _entity.TryGetMovementInputActionData(out _movementInputAction);
            
            OnInit();
        }

        public void OnInit()
        {
            SaveInputActionsToCache();

            //_subscribe = _inputActions.Subscribe(OnActionsChanged);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SaveInputActionsToCache()
        {
            _cache = _inputActions.CurrentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnActionsChanged(PlayerActions newActions)
        {
            DisableBaseMapIn(_cache);
            UnsubscribeFromMovementInput(_cache);
            DisposeInputActions(_cache);
            
            _cache = newActions;
            
            SubscribeToMovementInput(newActions);
            EnableBaseMapIn(newActions);
        }

        public void Awake()
        { 
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _cache ??= _inputActions.CurrentValue;
            
            SubscribeToMovementInput(_inputActions.CurrentValue);

            EnableBaseMapIn(_inputActions.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SubscribeToMovementInput(PlayerActions inputActions)
        {
            inputActions.BaseMap.Movement.performed += InvokeMovementInputAction;
            inputActions.BaseMap.Movement.canceled += CancelMovementInputAction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableBaseMapIn(PlayerActions inputActions)
        {
            inputActions.BaseMap.Enable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InvokeMovementInputAction(InputAction.CallbackContext context)
        {
            var vector2 = context.ReadValue<Vector2>();
            var direction = vector2.ToFloat2();
            _movementInputAction.Invoke(direction);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CancelMovementInputAction(InputAction.CallbackContext context)
        {
            _movementInputAction.Invoke(float2.zero);
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
            DisposeInputActions(_inputActions.CurrentValue);
            _subscribe?.Dispose();
            _cache = null;
        }
        
        public void Dispose()
        {
            UnsubscribeFromMovementInput(_inputActions.CurrentValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnsubscribeFromMovementInput(PlayerActions inputActions)
        {
            inputActions.BaseMap.Movement.performed -= InvokeMovementInputAction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableBaseMapIn(PlayerActions inputActions)
        {
            inputActions.BaseMap.Disable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisposeInputActions(PlayerActions inputActions)
        {
            inputActions.Dispose();
        }
    }
}