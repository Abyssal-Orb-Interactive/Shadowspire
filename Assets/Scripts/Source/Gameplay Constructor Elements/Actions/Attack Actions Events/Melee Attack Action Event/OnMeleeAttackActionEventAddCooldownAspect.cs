using System;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorFramework.Entity;
using Source.Gameplay_Constructor_Elements.Aspects;
using UnityEngine;

namespace GameplayConstructorElements.Actions.AttackActionsEvents.MeleeAttackActionEvent
{
    public class OnMeleeAttackActionEventAddCooldownAspect : IEntityActionFabric<float, DamageType>
    {
        [SerializeField] private AttackCooldownAspect _cooldownAspect = new();

        public OnMeleeAttackActionEventAddCooldownAspect()
        {
            _cooldownAspect = new AttackCooldownAspect();
        }
        
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (_,_) => _cooldownAspect.ApplyTo(entity);
        }
    }
}