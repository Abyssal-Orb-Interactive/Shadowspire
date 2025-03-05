using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Actions.AttackActionsEvents.MeleeAttackActionEvent
{
    [Serializable]
    public sealed class OnMeleeAttackActionEventInvokeInvincibilityStartAction : IEntityActionFabric<float, DamageType>
    {
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (_, _) =>
            {
                if (entity.TryGetAimData(out var aim))
                {
                    aim.TryInvokeInvincibility();
                }
                else if (entity.TryGetTargetsInDamageZoneData(out var targetsInDamageZone))
                {
                    var count = targetsInDamageZone.Count;

                    for (var i = 0; i < count; i++)
                    {
                        targetsInDamageZone[i].TryInvokeInvincibility();
                    }
                }
            };
        }
    }
}