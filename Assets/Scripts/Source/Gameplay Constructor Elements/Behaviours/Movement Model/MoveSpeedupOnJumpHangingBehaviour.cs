using System;
using System.Runtime.CompilerServices;
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
    public sealed class MoveSpeedupOnJumpHangingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;
        private bool _timerIsActive = false;
        
        #region Cache Variables
        
        private IReadonlyAtomicReactiveProperty<bool> _isJumpHanging = null;
        private IAtomicExpression<float> _speedExpression = null;
        private IAtomicValue<float> _jumpHangingMovementSpeedupModifier = null;
        private IReadonlyAtomicReactiveProperty<bool> _isMoving =  null;
        private IAtomicValue<float> _jumpHangingMovementSpeedupBonusDuration;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion
        
        #region Constructors

        public MoveSpeedupOnJumpHangingBehaviour() {}
        public MoveSpeedupOnJumpHangingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetIsJumpHangingData(out var isJumpHanging);
            _isJumpHanging = isJumpHanging;
            
            _entity.TryGetSpeedExpressionData(out var speedExpression);
            _speedExpression = speedExpression;
            
            _entity.TryGetJumpHangingMovementSpeedupModifierData(out var jumpHangingMovementSpeedupModifier);
            _jumpHangingMovementSpeedupModifier = jumpHangingMovementSpeedupModifier;
            
            _entity.TryGetIsMovingData(out var isMovingData);
            _isMoving = isMovingData;
            
            _entity.TryGetJumpHangingMovementSpeedupBonusDurationData(out var jumpHangingMovementSpeedupBonusDuration);
            _jumpHangingMovementSpeedupBonusDuration = jumpHangingMovementSpeedupBonusDuration;
            
            OnInit();
        }

        public void OnInit()
        {
            _timer = new Timer(_jumpHangingMovementSpeedupBonusDuration, _timeInvoker, TimerType.ScaledFrame);
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_isJumpHanging.Subscribe(OnJumpHangingChange));
            subscriptionBuilder.Add(_isMoving.Subscribe(OnMovingChange));
            subscriptionBuilder.Add(_timer.TimerFinished.Subscribe(OnTimerFinished));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnTimerFinished()
        {
            _speedExpression.Remove(GetSpeedupModifier);
            _timer.Stop();
            _timerIsActive = false;
        }

        private void OnJumpHangingChange(bool jumpHanging)
        {
            if(!jumpHanging || _timerIsActive || !_isMoving.CurrentValue) return;
            
            _speedExpression.Append(GetSpeedupModifier);
            _timerIsActive = true;
            _timer.Restart();
        }
        
        private void OnMovingChange(bool isMoving)
        {
            if(!isMoving || _timerIsActive || !_isJumpHanging.CurrentValue) return;
            
            _speedExpression.Append(GetSpeedupModifier);
            _timerIsActive = true;
            _timer.Restart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float GetSpeedupModifier()
        {
            return _jumpHangingMovementSpeedupModifier.CurrentValue <= 0f ? 0f : 1f + _jumpHangingMovementSpeedupModifier.CurrentValue;
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
            _timer.Dispose();
        }

        public void Dispose()
        {
            _timer.Stop();
            _timerIsActive = false;
            _subscription?.Dispose();
        }
        
        #endregion
    }
}