using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.AIModel
{
    [Serializable]
    public sealed class AutoAttackAimBehaviour : BehaviourBase, IInitBehaviour, IFrameRunBehaviour
    {
        #region Cache Variables

        private IAtomicValue<IEntity> _aim = null;
        private IAtomicValue<bool> _canAttack = null;
        private IAtomicAction<float, DamageType> _meleeAttackActionEvent = null;
        private IAtomicValue<float> _damage = null;
        private IAtomicValue<DamageType> _damageType = null;

        #endregion

        #region Constructors

        public AutoAttackAimBehaviour() {}
        public AutoAttackAimBehaviour(IEntity entity) : base(entity) {}

        #endregion
        
        public void Init()
        {
            _entity.TryGetAimData(out var aim);
            _aim = aim;
            
            _entity.TryGetCanAttackData(out var canAttack);
            _canAttack = canAttack;

            _entity.TryGetMeleeAttackActionEventData(out var meleeAttackActionEvent);
            _meleeAttackActionEvent = meleeAttackActionEvent;

            _entity.TryGetDamageData(out var damage);
            _damage = damage;
            
            _entity.TryGetDamageTypeData(out var damageType);
            _damageType = damageType;
            
            OnInit();
        }
        
        public void OnFrameRun()
        {
            if(_aim.CurrentValue == null || !_canAttack.CurrentValue) return;
            
            _meleeAttackActionEvent.Invoke(_damage.CurrentValue, _damageType.CurrentValue);
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
    }
}