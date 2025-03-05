using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using Source.GameplayConstructorElements.Aspects;
using UnityEngine;

namespace Source.Gameplay_Constructor_Elements.Actions.Invincibility_Start_Event_Actions
{
    [Serializable]
    public sealed class OnInvincibilityStartEventAddInvincibilityAspectAction : IEntityActionFabric
    {
        [SerializeField] private InvincibilityAspect _invincibilityAspect = new();
        
        public Action CreateFor(IEntity entity)
        {
            return () => _invincibilityAspect.ApplyTo(entity);
        }
    }
}