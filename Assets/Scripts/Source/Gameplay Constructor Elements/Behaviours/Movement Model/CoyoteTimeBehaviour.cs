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
    public sealed class CoyoteTimeBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;
        
        #region Cache Variables
        
        private ReactiveLibraryFacade.IObservable<bool> _isGrounded = null;
        private IAtomicValue<float> _coyoteTimeDuration = null;
        private IAtomicVariable<bool> _inCoyoteTime = null;
        private IAtomicValue<bool> _isJumping;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public CoyoteTimeBehaviour() {}
        public CoyoteTimeBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetIsGroundedData(out var isGrounded);
            _isGrounded = isGrounded;
            
            _entity.TryGetCoyoteTimeDurationData(out var coyoteTimeDuration);
            _coyoteTimeDuration = coyoteTimeDuration;
            
            _entity.TryGetIsInCoyoteTimeData(out var inCoyoteTime);
            _inCoyoteTime = inCoyoteTime;
            
            _entity.TryGetIsJumpingData(out var isJumping);
            _isJumping = isJumping;
            
            OnInit();
        }

        public void OnInit()
        {
            _timer = new Timer(_coyoteTimeDuration, _timeInvoker, TimerType.UnscaledFrame);
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_isGrounded.Subscribe(OnIsGroundedChange));
            subscriptionBuilder.Add(_timer.TimerFinished.Subscribe(OnTimerFinished));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnTimerFinished()
        {
            _timer.Stop();
            _inCoyoteTime.Value = false;
        }

        private void OnIsGroundedChange(bool isGrounded)
        {
            if (isGrounded || _isJumping.CurrentValue)
            {
                _inCoyoteTime.Value = false;
                return;
            }
            
            if (_inCoyoteTime.CurrentValue || _isJumping.CurrentValue) return;

            _timer.Restart();
            _inCoyoteTime.Value = true;
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
            _subscription = null;
        }
        
        #endregion
    }
}