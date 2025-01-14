using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class InvincibilityBehaviour : IInitBehaviour, IFrameRunBehaviour
    {
        private IEntity _entity = null;
        
        private IAtomicValue<float> _invincibilitySecondsDuration = null;
        private IAtomicVariable<bool> _invincibility = null;

        private float _elapsedInvincibilitySeconds = 0f;
        public InvincibilityBehaviour()
        {
            _entity = null;
        }

        public InvincibilityBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetInvincibilitySecondsDurationData(out var invincibilitySecondsDuration);
            _invincibilitySecondsDuration = invincibilitySecondsDuration;

            _entity.TryGetInvincibilityData(out var invincibility);
            _invincibility = invincibility;
            
            OnInit();
        }

        public void OnInit()
        {
        }

        public void OnFrameRun()
        {
            if (!_invincibility.CurrentValue) return;
            
            TimeCases.SetUpElapsedTimeByDeltaTime(ref _elapsedInvincibilitySeconds);
            
            if (_invincibilitySecondsDuration.CurrentValue > _elapsedInvincibilitySeconds) return;
            
            _invincibility.Value = false;
            _elapsedInvincibilitySeconds = 0f;
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
        }
    }
}