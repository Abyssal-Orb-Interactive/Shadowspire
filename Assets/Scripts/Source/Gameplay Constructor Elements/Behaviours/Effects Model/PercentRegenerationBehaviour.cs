using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Timers;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours.EffectsModel
{
    [Serializable]
    public sealed class PercentRegenerationBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicValue<float> _regenerationPercentPerSecond = null;
        private IAtomicValue<bool> _canRegenerate = null;
        private Timer _regenerationTimer = null;
        private IAtomicValue<float> _maxHealth = null;
        private IAtomicVariable<float> _health = null;
        private IAtomicAction _stopRegenerationAction = null;
        
        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion
        
        #region Constructors

        public PercentRegenerationBehaviour() {}
        public PercentRegenerationBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetMaxHealthData(out var maxHealth);
            _maxHealth = maxHealth;
            
            _entity.TryGetHealthData(out var health);
            _health = health;
            
            _entity.TryGetRegenerationPercentPerSecondData(out var regenerationPercentPerSecond);
            _regenerationPercentPerSecond = regenerationPercentPerSecond;
            
            _entity.TryGetCanRegenerateData(out var canRegenerate);
            _canRegenerate = canRegenerate;
            
            _entity.TryGetStopRegenerationData(out var stopRegenerationAction);
            _stopRegenerationAction = stopRegenerationAction;
            
            _entity.TryGetRegenerationTimerData(out _regenerationTimer);
            
            OnInit();
        }

        public void OnInit()
        {
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _regenerationTimer.ElapsedTimeInSeconds.Subscribe(OnTimerTick);
            
            _regenerationTimer.Start();
        }

        private void OnTimerTick(float elapsedTimeInSeconds)
        {
            if (elapsedTimeInSeconds <= 0f) return;
            
            if (!_canRegenerate.CurrentValue)
            {
                _stopRegenerationAction.Invoke();
                return;
            }
            
            _health.Value = HealCases.CalculateHealthAfterPercentHeal(_maxHealth, _health, _regenerationPercentPerSecond);
        }
        
        public void Sleep()
        {
            OnSleep();
        }

        public void OnSleep()
        {
            Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}