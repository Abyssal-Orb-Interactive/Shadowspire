using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UseCases;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class InvincibilityBehaviour : BehaviourBase, IInitBehaviour, IFrameRunBehaviour
    {
        private float _elapsedInvincibilitySeconds = 0f;
        
        #region Cache Varaibles

        private IAtomicValue<float> _invincibilitySecondsDuration = null;
        private IAtomicVariable<bool> _invincibility = null;

        #endregion
        
        #region Constructors
        
        public InvincibilityBehaviour() {}
        public InvincibilityBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
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
        
        #endregion
    }
}