using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class InvincibilityBehaviour : IInitBehaviour, IFrameRunBehaviour
    {
        private IEntity _entity = null;
        
        private IAtomicValue<float> _invincibilitySecondsDuration = null;

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
            
            OnInit();
        }

        public void OnInit()
        {
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
        }

        public void OnFrameRun()
        {
            _elapsedInvincibilitySeconds += Time.deltaTime;
            
            if (_invincibilitySecondsDuration.CurrentValue <= _elapsedInvincibilitySeconds)
            {
                
                _elapsedInvincibilitySeconds = 0f;
            }
        }
    }
}