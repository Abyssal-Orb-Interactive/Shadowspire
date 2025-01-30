using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorElements.Actions.EquipActionsEvents
{
    [Serializable]
    public sealed class OnLeftHandEquipActionEventSwitchLeftHandItem : IEntityActionFabric<IEntity>
    {
        public Action<IEntity> CreateFor(IEntity entity)
        {
            return newItem => { entity.TrySwitchLeftHandHandItemWith(newItem); };
        }
    }
}