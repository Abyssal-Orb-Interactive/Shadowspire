using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours.AttackModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Extensions;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;

namespace GameplayConstructorElements.Aspects
{
    [Serializable]
    public sealed class StunAspect  : IEntityAspect, IDisposable
    {
        [SerializeField] private AtomicReactiveProperty<float> _stunDuration = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isStunned = new();
        
        private Dictionary<int, IDisposable> _subscriptions = new();

        public StunAspect()
        {
            _stunDuration = new AtomicReactiveProperty<float>();
            _isStunned = new AtomicReactiveProperty<bool>();
            
            _subscriptions = new Dictionary<int, IDisposable>();
        }
        
        public StunAspect(in float duration, in bool isStunned)
        {
            _stunDuration = new AtomicReactiveProperty<float>(duration);
            _isStunned = new AtomicReactiveProperty<bool>(isStunned);
            
            _subscriptions = new Dictionary<int, IDisposable>();
        }

        public void ApplyTo(IEntity entity)
        {
            _subscriptions ??= new Dictionary<int, IDisposable>();
            
            entity.TryAddStunDurationData(_stunDuration);
            entity.TryAddIsStunnedData(_isStunned);
            
            var timer = new Timer(_stunDuration, TimeInvoker.Instance, TimerType.ScaledFrame);

            entity.TryAddStunTimerData(timer);
            
            entity.TryAddStunBehaviourBehaviour(new StunBehaviour(entity));
            
            if (_subscriptions.ContainsKey(entity.ID))
            {
                _subscriptions[entity.ID]?.Dispose();
                _subscriptions.Remove(entity.ID);
            }

            var subscription = timer.TimerFinished.Subscribe(() => OnTimerFinished(entity));
            
            _subscriptions.Add(entity.ID, subscription);
        }

        private void OnTimerFinished(IEntity entity)
        {
            DiscardFrom(entity);
        }

        public void DiscardFrom(IEntity entity)
        {
            _subscriptions[entity.ID]?.Dispose();
            _subscriptions.Remove(entity.ID);
            
            entity.TryRemoveStunBehaviourBehaviour();
            
            entity.TryRemoveStunTimerData();
            entity.TryRemoveStunDurationData();
            entity.TryRemoveIsStunnedData();
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