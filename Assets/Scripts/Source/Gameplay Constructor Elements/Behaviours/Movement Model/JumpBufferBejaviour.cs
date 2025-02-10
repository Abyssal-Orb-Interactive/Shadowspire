using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class JumpBufferBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;
        
        #region Cache Variables
        
        private IAtomicValue<float> _jumpBufferDuration = null;
        private IAtomicValue<bool> _canBufferingJump = null;
        private IAtomicValue<IEntity> _inputHandler = null;
        private IAtomicEvent<float> _jumpInputAction = null;
        private IAtomicValue<bool> _canJump;
        private ReactiveLibraryFacade.IObservable<bool> _isGrounded;
        private float _jumpModifierBuffer = 0f;

        #endregion
        
        #region Susbcriptions
        
        private IDisposable _subscription = null;
        private IDisposable _isGroundedSubscription = null;
        
        #endregion

        #region Constructors

        public JumpBufferBehaviour() {}
        public JumpBufferBehaviour(IEntity entity) : base(entity) {}

        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetJumpBufferingDurationData(out var jumpBufferDuration);
            _jumpBufferDuration = jumpBufferDuration;
            
            _entity.TryGetCanBufferingJumpData(out var canBufferingJump);
            _canBufferingJump = canBufferingJump;
            
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetCanJumpData(out var canJump);
            _canJump = canJump;
            
            _entity.TryGetIsGroundedData(out var isGrounded);
            _isGrounded = isGrounded;
            
            OnInit();
        }

        public void OnInit()
        {
            _timer = new Timer(_jumpBufferDuration.CurrentValue, _timeInvoker, TimerType.UnscaledFrame);
            
            _inputHandler.CurrentValue.TryGetJumpInputActionData(out var jumpInputAction);
            _jumpInputAction = jumpInputAction;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_jumpInputAction.Subscribe(OnJumpInput));
            subscriptionBuilder.Add(_timer.TimerFinished.Subscribe(OnTimerFinished));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnTimerFinished()
        {
            _timer.Stop();
            _jumpModifierBuffer = 0f;
            _isGroundedSubscription?.Dispose();
        }

        private void OnJumpInput(float jumpModifier)
        {
            if(_canJump.CurrentValue || !_canBufferingJump.CurrentValue) return;

            _isGroundedSubscription?.Dispose();
            _jumpModifierBuffer = jumpModifier;
            _isGroundedSubscription = _isGrounded.Subscribe(OnGrounded);
            _timer.Restart();
        }

        private void OnGrounded(bool isGrounded)
        {
            if(!isGrounded) return;
            
            _jumpInputAction.Invoke(_jumpModifierBuffer);
            
            _isGroundedSubscription?.Dispose();
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
            _isGroundedSubscription?.Dispose();
            _jumpModifierBuffer = 0f;
        }
        
        #endregion
    }
}