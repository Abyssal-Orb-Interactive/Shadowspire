using System;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorElements.Aspects;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Actions.AttackActionsEvents.MeleeAttackActionEvent
{
    [Serializable]
    public sealed class OnMeleeAttackActionEventAddStunAspect : IEntityActionFabric<float, DamageType>
    {
        [SerializeField] private StunAspect _stunAspect = new();

        public OnMeleeAttackActionEventAddStunAspect()
        {
            _stunAspect = new StunAspect();
        }
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (_, _) =>
            {
                if (entity.TryGetAimData(out var aim))
                {
                    _stunAspect.ApplyTo(aim.CurrentValue);
                }
                else if (entity.TryGetTargetsInDamageZoneData(out var targetsInDamageZone))
                {
                    var count = targetsInDamageZone.Count;

                    for (var i = 0; i < count; i++)
                    {
                        _stunAspect.ApplyTo(targetsInDamageZone[i]);
                    }
                }
            };
        }
    }
}