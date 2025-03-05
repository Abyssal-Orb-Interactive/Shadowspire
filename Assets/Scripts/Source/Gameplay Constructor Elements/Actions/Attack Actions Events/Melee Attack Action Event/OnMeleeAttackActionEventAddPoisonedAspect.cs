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
    public sealed class OnMeleeAttackActionEventAddPoisonedAspect : IEntityActionFabric<float, DamageType>
    {
        [SerializeField] private PoisonedAspect _poisonedAspect = new();

        public OnMeleeAttackActionEventAddPoisonedAspect()
        {
            _poisonedAspect = new PoisonedAspect();
        }
        
        public Action<float, DamageType> CreateFor(IEntity entity)
        {
            return (_, _) =>
            {
                if (entity.TryGetAimData(out var aim))
                {
                    _poisonedAspect.ApplyTo(aim.CurrentValue);
                }
                else if (entity.TryGetTargetsInDamageZoneData(out var targetsInDamageZone))
                {
                    var count = targetsInDamageZone.Count;

                    for (var i = 0; i < count; i++)
                    {
                        _poisonedAspect.ApplyTo(targetsInDamageZone[i]);
                    }
                }
            };
        }
    }
}