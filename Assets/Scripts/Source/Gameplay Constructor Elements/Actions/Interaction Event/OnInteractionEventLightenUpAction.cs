using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine.Rendering.Universal;

namespace GameplayConstructorElements.Actions.InteractionEvent
{
    [Serializable]
    public sealed class OnInteractionEventLightenUpAction : IEntityActionFabric<IEntity>, IDisposable
    {
        private IAtomicValue<Light2D> _light2D = null;
        private IAtomicValue<float> _targetFalloffStrength = null;
        private IAtomicValue<float> _maxFalloffStrength = null;
        private IAtomicVariable<bool> _isActive = null;

        private IDisposable _subscription = null;

        public Action<IEntity> CreateFor(IEntity entity)
        {
            return otherEntity =>
            {
                entity.TryGetLight2DCompponentData(out var light2D);
                _light2D = light2D;
                entity.TryGetMaxIgnitionFalloffStrengthData(out var maxFalloff);
                _maxFalloffStrength = maxFalloff;
                entity.TryGetTargetIgnitionFalloffStrengthData(out var targetFalloff);
                _targetFalloffStrength = targetFalloff;
                
                entity.TryGetIgnitionDurationData(out var ignitionDuration);
                
                entity.TryGetIsActiveData(out var isActive);
                _isActive = isActive;
                
                var timer = new Timer(ignitionDuration, TimeInvoker.Instance, TimerType.ScaledFrame);
                
                _light2D.CurrentValue.falloffIntensity = maxFalloff.CurrentValue;
                
                _light2D.CurrentValue.enabled = true;
                
                timer.Restart();
                
                var subscriptionBuilder = new DisposableBuilder();
                
                subscriptionBuilder.Add(timer.ElapsedTimeInPercents.Subscribe(OnTimerTick));
                subscriptionBuilder.Add(timer.TimerFinished.Subscribe(OnTimerFinished));
                
                _subscription = subscriptionBuilder.Build();
            };
        }

        private void OnTimerTick(float newPercent)
        {
            var falloffStrength = _maxFalloffStrength.CurrentValue - 
                                  (_maxFalloffStrength.CurrentValue - _targetFalloffStrength.CurrentValue) * newPercent;

            _light2D.CurrentValue.falloffIntensity = falloffStrength;
        }

        
        private void OnTimerFinished()
        {
            _isActive.Value = true;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
    }
}