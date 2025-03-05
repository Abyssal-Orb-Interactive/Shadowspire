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
            _entity.TryGetInvincibilitySecondsDurationData(out var invincibilitySecondsDuration);
            _invincibilitySecondsDuration = invincibilitySecondsDuration;

            _entity.TryGetInvincibilityData(out var invincibility);
            _invincibility = invincibility;
            
            _entity.TryGetInvincibilityTimerData(out _timer);
            
            OnInit();
        }

        public void OnInit()
        {}
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _timer.TimerFinished.Subscribe(OnTimerFinished);
            
            _timer.Start();
        }
        
        private void OnTimerFinished()
        {
            Destroy();
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