using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplayConstructorElements.Behaviours.InputHandlerModel
{
    [Serializable]
    public sealed class JumpInputHandlingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour ,IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;
        
        #region Cache Varaiables

        private IAtomicValue<PlayerActions> _inputActions = null;
        private IAtomicAction<float> _jumpInputAction = null;
        private IAtomicValue<float> _jumpInputKeyMaxPressingTime = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion
        
        #region Constructors

        public JumpInputHandlingBehaviour() {}

        public JumpInputHandlingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life cycle Methods
        public void Init()
        {
            _entity.TryGetJumpInputKeyMaxPressingTimeData(out var jumpInputKeyMaxPressingTime);
            _jumpInputKeyMaxPressingTime = jumpInputKeyMaxPressingTime;
            
            _timeInvoker = TimeInvoker.Instance;
            _timer = new Timer(_jumpInputKeyMaxPressingTime.CurrentValue, _timeInvoker, TimerType.UnscaledFrame);
            
            _entity.TryGetInputActionsData(out var inputActions);
            _inputActions = inputActions;
            
            _entity.TryGetJumpInputActionData(out var jumpInputAction);
            _jumpInputAction = jumpInputAction;
            
            
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
            _inputActions.CurrentValue.BaseMap.Jump.started += OnStartJumpInput;
            _inputActions.CurrentValue.BaseMap.Jump.canceled += OnCancelJumpInput;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnStartJumpInput(InputAction.CallbackContext callbackContext)
        {
            _subscription?.Dispose();
            _subscription = _timer.TimerFinished.Subscribe(InvokeJumpAction);
            _timer.Restart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnCancelJumpInput(InputAction.CallbackContext callbackContext)
        {
            InvokeJumpAction();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InvokeJumpAction()
        {
            _jumpInputAction.Invoke(_timer.ElapsedTimeInPercents.CurrentValue);
            _subscription?.Dispose();
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
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
            _inputActions.CurrentValue.BaseMap.Jump.started -= OnStartJumpInput;
            _inputActions.CurrentValue.BaseMap.Jump.canceled -= OnCancelJumpInput;
            _subscription = null;
        }
        
        #endregion
    }
}