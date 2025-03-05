using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours.EffectsModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Extensions;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameplayConstructorElements.Aspects
{
    [Serializable]
    public sealed class PoisonedAspect : IEntityAspect, IDisposable
    {
        [SerializeField] private AtomicReactiveProperty<float> _poisoningDuration = new();
        [SerializeField] private AtomicReactiveProperty<float> _poisoningDamageInPercents = new();
        [SerializeField] private AtomicReactiveProperty<float> _startPoisoningDamageModifier = new();
        [SerializeField] private AtomicReactiveProperty<float> _poisoningDamageGrowthModifier = new();
        [SerializeField] private AtomicReactiveProperty<int> _numberOfPoisoningEffects = new(1);
        [SerializeField] private AtomicReactiveProperty<int> _maxNumberOfPoisoningEffects = new(1);
        [SerializeField] private AtomicEvent _poisoningContinued = new();
        [SerializeReference] private IEntityConditionFabric[] _canBePoisonedConditionFabrics = Array.Empty<IEntityConditionFabric>();
        [SerializeReference] private IEntityFloatExpressionElementFabric[] _poisonDamageExpressionFabrics = Array.Empty<IEntityFloatExpressionElementFabric>();

        private Dictionary<int, IDisposable> _subscriptions = new();
        
        public PoisonedAspect()
        {
            _poisoningDuration = new AtomicReactiveProperty<float>(0);
            _poisoningDamageInPercents = new AtomicReactiveProperty<float>(0);
            _startPoisoningDamageModifier = new AtomicReactiveProperty<float>(0);
            _poisoningDamageGrowthModifier = new AtomicReactiveProperty<float>(0);
            _numberOfPoisoningEffects = new AtomicReactiveProperty<int>(1);
            _maxNumberOfPoisoningEffects = new AtomicReactiveProperty<int>(1);
            _poisoningContinued = new AtomicEvent();
            _canBePoisonedConditionFabrics = Array.Empty<IEntityConditionFabric>();
            _poisonDamageExpressionFabrics = Array.Empty<IEntityFloatExpressionElementFabric>();
            
            _subscriptions = new Dictionary<int, IDisposable>();
        }
        
        public void ApplyTo(IEntity entity)
        {
            _subscriptions ??= new Dictionary<int, IDisposable>();
            
            if (entity.TryGetNumberOfPoisoningEffectsData(out var numberOfPoisoningEffects))
            {
                numberOfPoisoningEffects.Value++;
                entity.TryGetPoisoningEffectContinuedData(out var poisoningContinued);
                poisoningContinued.Invoke();
                return;
            }
            
            entity.TryAddPoisoningEffectDurationData(_poisoningDuration);
            entity.TryAddPoisoningEffectDamageInPercentsData(_poisoningDamageInPercents);
            entity.TryAddStartPoisoningDamageModifierData(_startPoisoningDamageModifier);
            entity.TryAddPoisoningDamageGrowthModifierData(_poisoningDamageGrowthModifier);
            entity.TryAddNumberOfPoisoningEffectsData(_numberOfPoisoningEffects);
            entity.TryAddMaxNumberOfPoisoningEffectsData(_maxNumberOfPoisoningEffects);
            entity.TryAddPoisoningEffectContinuedData(_poisoningContinued);

            var canBePoisoned = new AtomicBoolMultiplication();
            canBePoisoned.AppendBy(_canBePoisonedConditionFabrics, entity);
            entity.TryAddCanBePoisenedData(canBePoisoned);

            var poisonDamageExpression = new AtomicFloatMultiplication();
            poisonDamageExpression.AppendBy(_poisonDamageExpressionFabrics, entity);
            entity.TryAddPoisoningEffectDamageExpressionData(poisonDamageExpression);
            
            var timer = new Timer(_poisoningDuration, TimeInvoker.Instance, TimerType.ScaledSecond);
            
            entity.TryAddPoisoningEffectTimerData(timer);

            if (_subscriptions.ContainsKey(entity.ID))
            {
                _subscriptions[entity.ID]?.Dispose();
                _subscriptions.Remove(entity.ID);
            }
            
            var subscription = timer.TimerFinished.Subscribe(() => OnTimerFinished(entity));

            _subscriptions.Add(entity.ID, subscription);
            
            entity.TryAddPoisoningBehaviourBehaviour(new PoisoningBehaviour(entity));
        }

        private void OnTimerFinished(IEntity entity)
        {
            DiscardFrom(entity);
        }

        public void DiscardFrom(IEntity entity)
        {
            _subscriptions[entity.ID]?.Dispose();
            _subscriptions.Remove(entity.ID);
            
            entity.TryRemovePoisoningBehaviourBehaviour();
            
            entity.TryRemovePoisoningEffectDurationData();
            entity.TryRemovePoisoningEffectDamageInPercentsData();
            entity.TryRemoveStartPoisoningDamageModifierData();
            entity.TryRemovePoisoningDamageGrowthModifierData();
            entity.TryRemoveNumberOfPoisoningEffectsData();
            entity.TryRemoveMaxNumberOfPoisoningEffectsData();
            entity.TryRemovePoisoningEffectContinuedData();
            entity.TryRemoveCanBePoisenedData();
            entity.TryRemovePoisoningEffectDamageExpressionData();
            entity.TryRemovePoisoningEffectTimerData();
        }
        
        
        public void Dispose() 
        {
            foreach (var subscription in _subscriptions.Values)
            {
                subscription?.Dispose();
            }
            
            _subscriptions.Clear();
            _subscriptions = null;
        }
    }
}