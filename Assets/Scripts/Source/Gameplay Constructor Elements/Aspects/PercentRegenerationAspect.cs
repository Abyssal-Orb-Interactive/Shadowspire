using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours.EffectsModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Extensions;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;

namespace GameplayConstructorElements.Aspects
{
    [Serializable]
    public class PercentRegenerationAspect : IEntityAspect, IDisposable
    {
        [SerializeField] private AtomicReactiveProperty<float> _regenerationDurationInSeconds = new();
        [SerializeField] private AtomicReactiveProperty<float> _regenerationPercentPerSecond = new();
        [SerializeReference] private IEntityConditionFabric[] _canRegenerateConditionFabrics = Array.Empty<IEntityConditionFabric>();
        [SerializeField] private AtomicEvent _stopRegeneration = new();
        
        private Dictionary<int, IDisposable> _subscriptions = new();

        public PercentRegenerationAspect()
        {
            _regenerationDurationInSeconds = new AtomicReactiveProperty<float>();
            _regenerationPercentPerSecond = new AtomicReactiveProperty<float>();
            _canRegenerateConditionFabrics = Array.Empty<IEntityConditionFabric>();
            _stopRegeneration = new AtomicEvent();
            
            _subscriptions = new Dictionary<int, IDisposable>();
        }
        
        public void ApplyTo(IEntity entity)
        {
            _subscriptions ??= new Dictionary<int, IDisposable>();

            if (entity.TryGetRegenerationTimerData(out var timer))
            {
                DiscardFrom(entity);
            }
            
            entity.TryAddRegenerationDurationInSecondsData(_regenerationDurationInSeconds);
            entity.TryAddRegenerationPercentPerSecondData(_regenerationPercentPerSecond);
            entity.TryAddStopRegenerationData(_stopRegeneration);

            var canRegenerate = new AtomicBoolMultiplication();
            canRegenerate.AppendBy(_canRegenerateConditionFabrics, entity);
            entity.TryAddCanRegenerateData(canRegenerate);
            
            var regenerationTimer = new Timer(_regenerationDurationInSeconds, TimeInvoker.Instance, TimerType.ScaledSecond);
            entity.TryAddRegenerationTimerData(regenerationTimer);
            
            if (_subscriptions.ContainsKey(entity.ID))
            {
                _subscriptions[entity.ID]?.Dispose();
                _subscriptions.Remove(entity.ID);
            }

            var subscriptionBuilder = new DisposableBuilder();
            subscriptionBuilder.Add(regenerationTimer.TimerFinished.Subscribe(() => OnTimerFinished(entity)));
            subscriptionBuilder.Add(_stopRegeneration.Subscribe(() => OnStopRegeneration(entity)));
            var subscription = subscriptionBuilder.Build();
            
            _subscriptions.Add(entity.ID, subscription);
            
            entity.TryAddPercentRegenerationBehaviourBehaviour(new PercentRegenerationBehaviour(entity));
        }

        private void OnStopRegeneration(IEntity entity)
        {
            DiscardFrom(entity);
        }

        private void OnTimerFinished(IEntity entity)
        {
            DiscardFrom(entity);
        }

        public void DiscardFrom(IEntity entity)
        {
            if (_subscriptions.ContainsKey(entity.ID))
            {
                _subscriptions[entity.ID]?.Dispose();
                _subscriptions.Remove(entity.ID);  
            }
            
            
            entity.TryRemovePercentRegenerationBehaviourBehaviour();

            entity.TryRemoveRegenerationTimerData();
            entity.TryRemoveStopRegenerationData();
            entity.TryRemoveCanRegenerateData();
            entity.TryRemoveRegenerationPercentPerSecondData();
            entity.TryRemoveRegenerationDurationInSecondsData();
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