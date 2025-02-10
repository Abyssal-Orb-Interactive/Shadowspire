using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorElements.Actions.DeathEvent
{
    [Serializable]
    public sealed class OnDeathEventDroppingInventoryAction : IEntityActionFabric
    {
        public Action CreateFor(IEntity entity)
        {
            return () => { entity.TryDropAllInventory(); };
        }
    }
}