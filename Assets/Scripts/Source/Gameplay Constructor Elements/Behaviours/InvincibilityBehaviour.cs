using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using UseCases;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class InvincibilityBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;
        
        #region Cache Varaibles

        private IAtomicValue<float> _invincibilitySecondsDuration = null;
        private AtomicReactiveProperty<bool> _invincibility = null;

        #endregion

        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors
        
        public InvincibilityBehaviour() {}
        public InvincibilityBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetInvincibilitySecondsDurationData(out var invincibilitySecondsDuration);
            _invincibilitySecondsDuration = invincibilitySecondsDuration;

            _entity.TryGetInvincibilityData(out var invincibility);
            _invincibility = invincibility;
            
            OnInit();
        }

        public void OnInit()
        {
            _timer = new Timer(_invincibilitySecondsDuration, _timeInvoker, TimerType.ScaledFrame);
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_invincibility.Subscribe(OnInvincibilityChange));
            subscriptionBuilder.Add(_timer.TimerFinished.Subscribe(OnTimerFinished));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnInvincibilityChange(bool invincibility)
        {
            if (!invincibility)
            {
                _timer.Stop();
                return;
            }
            
            _timer.Restart();
        }
        
        private void OnTimerFinished()
        {
            _invincibility.Value = false;
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
            _timer?.Dispose();
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}