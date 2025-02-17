using System;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Actions.AttackActionsEvents.MeleeAttackActionEvent
{
    [Serializable]
    public sealed class OnMeleeAttackActionEventTargetDamageAction : IEntityActionFabric<float, DamageType>
    {
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (damage, damageType) =>
            {
                entity.TryAttackAimInDamageZoneWith(damage, damageType);
            };
        }
    }
}