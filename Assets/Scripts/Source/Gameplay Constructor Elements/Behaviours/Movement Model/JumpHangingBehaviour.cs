using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.UnityExtensions;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using Unity.Mathematics;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class JumpHangingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IFrameRunBehaviour ,IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _jumpHangingTimer = null;
        
        #region Cache Variables
        
        private IAtomicValue<bool> _canJimpHanging = null;
        private IAtomicValue<float> _jumpHangingDuration = null;
        private IAtomicValue<float> _jumpSpeedupModifier = null;
        private IAtomicValue<float> _jumpSlowModifier = null;
        private IAtomicValue<Rigidbody2D> _rigidbody2D = null;
        private IAtomicValue<float> _velocityThresholdToJumpHanging = null;
        private AtomicReactiveProperty<bool> _isJumpHanging = null;
        private ReactiveLibraryFacade.IObservable<bool> _isGrounded = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public JumpHangingBehaviour() {}
        public JumpHangingBehaviour(IEntity entity) : base(entity) {}

        #endregion
        
        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetCanJumpHangingData(out var canJimpHanging);
            _canJimpHanging = canJimpHanging;
            
            _entity.TryGetJumpHangingDurationData(out var jumpHangingDuration);
            _jumpHangingDuration = jumpHangingDuration;
            
            _entity.TryGetJumpSpeedupModifierData(out var jumpSpeedupModifier);
            _jumpSpeedupModifier = jumpSpeedupModifier;
            
            _entity.TryGetJumpSlowModifierData(out var jumpSlowModifier);
            _jumpSlowModifier = jumpSlowModifier;
            
            _entity.TryGetRigidbody2DData(out var rigidbody2D);
            _rigidbody2D = rigidbody2D;
            
            _entity.TryGetVelocityThresholdToJumpHangingData(out var velocityThresholdToJumpHanging);
            _velocityThresholdToJumpHanging = velocityThresholdToJumpHanging;
            
            _entity.TryGetIsJumpHangingData(out var isJumpHanging);
            _isJumpHanging = isJumpHanging;
            
            _entity.TryGetIsGroundedData(out var isGrounded);
            _isGrounded = isGrounded;
            
            OnInit();
        }

        public void OnInit()
        {
            _jumpHangingTimer = new Timer(_jumpHangingDuration.CurrentValue, _timeInvoker, TimerType.ScaledFrame);
        }
        
        [Obsolete("Obsolete")]
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        [Obsolete("Obsolete")]
        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_jumpHangingTimer.TimerFinished.Subscribe(OnTimerFinished));
            subscriptionBuilder.Add(_jumpHangingTimer.ElapsedTimeInSeconds.Subscribe(OnTimerTick));
            subscriptionBuilder.Add(_isJumpHanging.Subscribe(OnJumpHangingChange));
            subscriptionBuilder.Add(_isGrounded.Subscribe(OnGroundedChange));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnTimerFinished()
        {
            _isJumpHanging.Value = false;
        }
        
        [Obsolete("Obsolete")]
        private void OnTimerTick(float newTime)
        {
            var rb = _rigidbody2D.CurrentValue;
            var velocity = rb.velocity.ToFloat2();
            var yComponent = velocity.y;
            
            if (math.abs(yComponent) <= _velocityThresholdToJumpHanging.CurrentValue)
            {
                rb.velocity = yComponent switch
                {
                    > 0f => MovementCases.CalculateIncreasedYVelocityWithModifer(velocity, _jumpSpeedupModifier),
                    < 0f => MovementCases.CalculateDecreasedYVelocityWithModifer(velocity, _jumpSlowModifier),
                    _ => velocity
                };
            }
        }

        private void OnJumpHangingChange(bool isJumpHanging)
        {
            if (isJumpHanging) return;
            
            _jumpHangingTimer.Stop();
        }
        
        private void OnGroundedChange(bool isGrounded)
        {
            if(isGrounded) _isJumpHanging.Value = false;
        }

        public void OnFrameRun()
        {
            if(!_canJimpHanging.CurrentValue) return;
            
            _jumpHangingTimer.Start();
            _isJumpHanging.Value = true;
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
            _jumpHangingTimer?.Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}