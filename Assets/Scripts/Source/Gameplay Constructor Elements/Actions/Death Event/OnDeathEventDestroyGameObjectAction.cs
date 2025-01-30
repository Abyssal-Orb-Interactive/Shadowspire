using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorElements.Actions.DeathEvent
{
    [Serializable]
    public sealed class OnDeathEventDestroyGameObjectAction : IEntityActionFabric
    {
        public Action CreateFor(IEntity entity)
        {
            return entity.Dispose;
        }
    }
}