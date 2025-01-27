using System;
using System.Collections.Generic;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade;

namespace GameplayConstructorElements.Behaviours.AttackModel
{
    [Serializable]
    public sealed class SplashMeleeAttackBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IReadOnlyList<IEntity> _targetsInDamageZone = null;
        private IObservable<float, DamageType> _meleeAttackActionEvent = null;
        
        private IDisposable _subscription = null;

        public SplashMeleeAttackBehaviour()
        {
            _entity = new Entity();
        }

        public SplashMeleeAttackBehaviour(IEntity entity)
        {
            _entity = entity;
        }
        public void Init()
        {
            _entity.TryGetTargetsInDamageZoneData(out var targetsInDamageZone);
            _targetsInDamageZone = targetsInDamageZone;

            _entity.TryGetMeleeAttackActionEventData(out var meleeAttackActionEvent);
            _meleeAttackActionEvent = meleeAttackActionEvent;
            
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
            _subscription = _meleeAttackActionEvent.Subscribe(OnMeleeAttack);
        }

        private void OnMeleeAttack(float damage, DamageType damageType)
        {
            var count = _targetsInDamageZone.Count;

            for (var i = 0; i < count; i++)
            {
                _targetsInDamageZone[i].TryTakeDamage(damage);
            }
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
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}