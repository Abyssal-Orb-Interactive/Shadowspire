using System;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorElements.Actions.AttackActionsEvents.MeleeAttackActionEvent
{
    [Serializable]
    public sealed class OnMeleeAttackActionEventSplashAction : IEntityActionFabric<float, DamageType>
    {
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (damage, damageType) => { entity.TrySplashAttackAllTargetsInDamageZoneWith(damage, damageType); };
        }
    }
}