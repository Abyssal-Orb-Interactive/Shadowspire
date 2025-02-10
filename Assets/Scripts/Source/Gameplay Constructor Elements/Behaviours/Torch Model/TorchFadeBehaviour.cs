using System;
using System.Globalization;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameplayConstructorElements.Behaviours.TorchModel
{
    [Serializable]
    public sealed class TorchFadeBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private TimeInvoker _timeInvoker = null;
        private Timer _timer = null;

        #region Cache Variables

        private IAtomicValue<float> _fadeDuration = null;
        private IAtomicVariable<float> _currentFalloffStrength = null;
        private IAtomicValue<float> _minFalloffStrength = null;
        private IAtomicValue<float> _targetFalloffStrength = null;
        private IAtomicValue<Light2D> _light2D = null;
        private IReadonlyAtomicReactiveProperty<bool> _isActive = null;

        #endregion

        #region Subscriptiions

        private IDisposable _subscription = null;

        #endregion

        #region Constructors

        public TorchFadeBehaviour() {}

        public TorchFadeBehaviour(IEntity entity) : base(entity) {}

        #endregion

        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetFadeDurationData(out var fadeDuration);
            _fadeDuration = fadeDuration;
            
            _entity.TryGetCurrentFalloffStrenghtData(out var currentFalloffStrength);
            _currentFalloffStrength = currentFalloffStrength;
            
            _entity.TryGetTargetFalloffStrenghtData(out var targetFalloffStrength);
            _targetFalloffStrength = targetFalloffStrength;
            
            _entity.TryGetMinFalloffStrenghtData(out var minFalloffStrength);
            _minFalloffStrength = minFalloffStrength;
            
            _entity.TryGetLight2DCompponentData(out var light2D);
            _light2D = light2D;
            
            _entity.TryGetIsActiveData(out var isActive);
            _isActive = isActive;
            
            OnInit();
        }

        public void OnInit()
        {
            _timer = new Timer(_fadeDuration.CurrentValue, _timeInvoker , TimerType.ScaledFrame);
        }
        
        public void Awake()
        {
            Dispose(); 
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_timer.ElapsedTimeInPercents.Subscribe(OnTimerTick));
            subscriptionBuilder.Add(_timer.TimerFinished.Subscribe(OnTimerFinished));
            subscriptionBuilder.Add(_isActive.Subscribe(OnActivation));
            
            _subscription = subscriptionBuilder.Build();
        }
        
        private void OnTimerFinished()
        {
            _timer.Restart();
        }
        
        private void OnTimerTick(float newPercent)
        {
            if(!_isActive.CurrentValue) return; 
            
            if (newPercent <= 0.5f)
                _currentFalloffStrength.Value = _minFalloffStrength.CurrentValue + 
                                                (_targetFalloffStrength.CurrentValue - _minFalloffStrength.CurrentValue) * (newPercent / 0.5f);
            else
                _currentFalloffStrength.Value = _targetFalloffStrength.CurrentValue - 
                                                (_targetFalloffStrength.CurrentValue - _minFalloffStrength.CurrentValue) * ((newPercent - 0.5f) / 0.5f);

            _light2D.CurrentValue.falloffIntensity = _currentFalloffStrength.CurrentValue;
        }
        
        private void OnActivation(bool isActive)
        {
            if(!isActive) return;
            
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
    }
}