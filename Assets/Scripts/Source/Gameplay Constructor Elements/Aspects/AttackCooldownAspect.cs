using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours.Attack_Model;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Extensions;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Core;
using TimeFramework.Timers;
using UnityEngine;

namespace Source.Gameplay_Constructor_Elements.Aspects
{
    [Serializable]
    public sealed class AttackCooldownAspect : IEntityAspect, IDisposable
    {
        [SerializeField] private AtomicReactiveProperty<float> _attackCooldown = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isInAttackCooldown = new();
        
        private Dictionary<int, IDisposable> _subscriptions = new();
        
        public AttackCooldownAspect()
        {
            _attackCooldown = new AtomicReactiveProperty<float>();
            _isInAttackCooldown = new AtomicReactiveProperty<bool>();
            _subscriptions = new Dictionary<int, IDisposable>();
        }

        public AttackCooldownAspect(in float cooldown, in bool isInCooldown)
        {
            _attackCooldown = new AtomicReactiveProperty<float>(cooldown);
            _isInAttackCooldown = new AtomicReactiveProperty<bool>(isInCooldown);
            _subscriptions = new Dictionary<int, IDisposable>();
        }
        
        public void ApplyTo(IEntity entity)
        {
            _subscriptions ??= new Dictionary<int, IDisposable>();
            
            entity.TryAddAttackCooldownData(_attackCooldown);
            entity.TryAddIsInAttackCooldownData(_isInAttackCooldown);
            
            var timer = new Timer(_attackCooldown, TimeInvoker.Instance, TimerType.ScaledFrame);

            entity.TryAddCoolodownTimerData(timer);
            
            entity.TryAddAttackCooldownBehaviourBehaviour(new AttackCooldownBehaviour(entity));
            
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
            
            entity.TryRemoveAttackCooldownBehaviourBehaviour();
            
            entity.TryRemoveCoolodownTimerData();
            entity.TryRemoveAttackCooldownData();
            entity.TryRemoveIsInAttackCooldownData();
            
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