using System;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Actions.InputActionsEvents.UseInputActionEvent
{
    [Serializable]
    public sealed class OnUseInputActionEventMeleeAttackAction : IEntityActionFabric
    {
        public Action CreateFor(IEntity entity)
        {
            return () =>
            {
                entity.TryGetUserData(out var user);
                user.TryUseInMeleeAttack(entity);
            };
        }
    }
}