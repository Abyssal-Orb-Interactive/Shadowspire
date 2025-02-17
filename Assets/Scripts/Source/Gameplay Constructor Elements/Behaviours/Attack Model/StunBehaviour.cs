using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Timers;

namespace GameplayConstructorElements.Behaviours.AttackModel
{
    [Serializable]
    public sealed class StunBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables
        
        private Timer _stunTimer = null;
        private IAtomicVariable<bool> _isStunned  = null;

        #endregion

        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public StunBehaviour() {}
        public StunBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods

        public void Init()
        {
            _entity.TryGetStunTimerData(out var stunTimer);
            _stunTimer = stunTimer;
            
            _entity.TryGetIsStunnedData(out var isStunned);
            _isStunned = isStunned;
            
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
            _subscription = _stunTimer.TimerFinished.Subscribe(OnStunFinished);
            _isStunned.Value = true;
            _stunTimer.Start();
        }

        private void OnStunFinished()
        {
            _isStunned.Value = false;
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
            _stunTimer?.Dispose();
            _stunTimer = null;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}