using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;

namespace Source.Gameplay_Constructor_Elements.Actions.Interaction_Event
{
    [Serializable]
    public sealed class OnInteractionEventPickUpAction : IEntityActionFabric<IEntity>
    {
        public Action<IEntity> CreateFor(IEntity entity)
        {
            return otherEntity => { otherEntity.TryPickUp(entity); };
        }
    }
}