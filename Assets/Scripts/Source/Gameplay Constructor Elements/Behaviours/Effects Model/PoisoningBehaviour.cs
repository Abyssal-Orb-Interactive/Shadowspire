using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours.EffectsModel
{
    [Serializable]
    public sealed class PoisoningBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {

        #region CacheVariables

        private IAtomicValue<float> _poisoningDuration = null;
        private IAtomicValue<bool> _canBePoisoning = null;
        private IAtomicValue<float> _poisoningDamage = null;
        private ReactiveLibraryFacade.IObservable _poisoningContinued = null;
        private Timer _timer = null;

        #endregion

        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion

        #region Constructors

        public PoisoningBehaviour() {}
        public PoisoningBehaviour(IEntity entity) : base(entity) {}

        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetPoisoningEffectDurationData(out var poisoningDuration);
            _poisoningDuration = poisoningDuration;
            
            _entity.TryGetCanBePoisenedData(out var canBePoisoning);
            _canBePoisoning = canBePoisoning;
            
            _entity.TryGetPoisoningEffectDamageExpressionData(out var poisoningDamage);
            _poisoningDamage = poisoningDamage;
            
            _entity.TryGetPoisoningEffectContinuedData(out var poisoningContinued);
            _poisoningContinued = poisoningContinued;

            _entity.TryGetPoisoningEffectTimerData(out _timer);
            
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
            var subscriptionBuilder = new DisposableBuilder();
            subscriptionBuilder.Add(_timer.ElapsedTimeInSeconds.Subscribe(OnTimerTick));
            subscriptionBuilder.Add(_poisoningContinued.Subscribe(OnPoisoningContinued));
            
            _subscription = subscriptionBuilder.Build();
            
            _timer.Start();
        }

        private void OnTimerTick(float elapsedTimeInSeconds)
        {
            if (elapsedTimeInSeconds <= 0f) return;
            if (!_canBePoisoning.CurrentValue) return;

            _entity.TryTakeDamageByPercent(_poisoningDamage);
        }

        private void OnPoisoningContinued()
        {
            _timer.Restart();
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