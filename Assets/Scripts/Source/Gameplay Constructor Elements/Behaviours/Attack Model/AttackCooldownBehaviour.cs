using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Timers;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.Attack_Model
{
    [Serializable]
    public sealed class AttackCooldownBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicVariable<bool> _isInAttackCooldown = null;
        private Timer _timer = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public AttackCooldownBehaviour() {}
        public AttackCooldownBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetIsInAttackCooldownData(out var isInAttackCooldown);
            _isInAttackCooldown = isInAttackCooldown;
            
            _entity.TryGetCoolodownTimerData(out var timer);
            _timer = timer;
            
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
            _subscription = _timer.TimerFinished.Subscribe(OnCooldownFinished);
            _isInAttackCooldown.Value = true;
            _timer.Start();
        }
        
        private void OnCooldownFinished()
        {
            _isInAttackCooldown.Value = false;
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