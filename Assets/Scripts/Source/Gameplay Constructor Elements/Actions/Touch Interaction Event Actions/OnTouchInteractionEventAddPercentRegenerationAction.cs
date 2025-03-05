using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorElements.Aspects;
using GameplayConstructorFramework.Entity;
using UnityEngine;

namespace GameplayConstructorElements.Actions.TouchInteractionEventActions
{
    [Serializable]
    public sealed class OnTouchInteractionEventAddPercentRegenerationAction : IEntityActionFabric<IEntity>
    {
        [SerializeField] private PercentRegenerationAspect _percentRegenerationAspect = new();
        public Action<IEntity> CreateFor(IEntity entity)
        {
            return otherEntity => { _percentRegenerationAspect.ApplyTo(otherEntity); };
        }
    }
}