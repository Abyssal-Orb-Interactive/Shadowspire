using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Extensions;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;

namespace Source.GameplayConstructorElements.Aspects
{
    [Serializable]
    public sealed class InvincibilityAspect : IEntityAspect, IDisposable
    {
        [SerializeField] private AtomicReactiveProperty<bool> _invincibility = new();
        [SerializeField] private AtomicReactiveProperty<float> _invincibilitySecondsDuration = new();
        
        private Dictionary<int, IDisposable> _subscriptions = new();

        public InvincibilityAspect()
        {
            _invincibility = new AtomicReactiveProperty<bool>();
            _invincibilitySecondsDuration = new AtomicReactiveProperty<float>();
            
            _subscriptions = new Dictionary<int, IDisposable>();
        }
        
        public void ApplyTo(IEntity entity)
        {
            _subscriptions ??= new Dictionary<int, IDisposable>();
            
            entity.TryAddInvincibilityData(_invincibility);
            entity.TryAddInvincibilitySecondsDurationData(_invincibilitySecondsDuration);

            var timer = new Timer(_invincibilitySecondsDuration, TimeInvoker.Instance, TimerType.ScaledFrame);
            entity.TryAddInvincibilityTimerData(timer);
            
            if (_subscriptions.ContainsKey(entity.ID))
            {
                _subscriptions[entity.ID]?.Dispose();
                _subscriptions.Remove(entity.ID);
            }
            
            var subscription = timer.TimerFinished.Subscribe(() => OnTimerFinished(entity));

            _subscriptions.Add(entity.ID, subscription);
            
            entity.TryAddInvincibilityBehaviourBehaviour(new InvincibilityBehaviour(entity));
        }
        
        private void OnTimerFinished(IEntity entity)
        {
            DiscardFrom(entity);
        }

        public void DiscardFrom(IEntity entity)
        {
            _subscriptions[entity.ID]?.Dispose();
            _subscriptions.Remove(entity.ID);   
            
            entity.TryRemoveInvincibilityBehaviourBehaviour();
            
            entity.TryRemoveInvincibilityTimerData();
            entity.TryRemoveInvincibilityData();
            entity.TryRemoveInvincibilitySecondsDurationData();
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