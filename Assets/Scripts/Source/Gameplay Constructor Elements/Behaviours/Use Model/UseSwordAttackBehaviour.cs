using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.Use_Model
{
   
    public class UseSwordAttackBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private readonly IReadOnlyList<IEntity> _targets = null;
        private IAtomicValue<IEntity> _user = null;
        private IReadonlyAtomicReactiveProperty<IEntity> _inputHandler = null;
        private IAtomicValue<float> _damage = null;
        private AtomicEvent _useInputActionEvent;

        
        private IDisposable _subscription = null;

        public UseSwordAttackBehaviour()
        {
            _entity = new Entity();
        }

        public UseSwordAttackBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetUserData(out var user);
            _user = user;
            
            OnInit();
        }

        public void OnInit()
        {
            var user = _user.CurrentValue;
            
            user.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _inputHandler.CurrentValue.TryGetUseInputActionData(out var useInputActionEvent);
            _useInputActionEvent = useInputActionEvent;
            
        }
        
        public void Awake()
        {
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _useInputActionEvent.Subscribe(OnUseInputAction);
        }

        private void OnUseInputAction()
        {
            _entity.UseInMeleeAttack();
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