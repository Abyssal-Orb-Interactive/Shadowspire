using System;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Actions.EnemyInVisionEventActions
{
    [Serializable]
    public sealed class EnemyInVisionEventRotateToEnemyAction : IEntityActionFabric<IEntity>
    {
        public Action<IEntity> CreateFor(IEntity entity)
        {
            return enemy =>
            {
                if(enemy == null) return;
                
                if (!enemy.TryGetTransformData(out var enemyTransform) || 
                    !entity.TryGetTransformData(out var transform) || 
                    !entity.TryGetCurrentFacingData(out var facing) )
                    return;
                
                var direction = enemyTransform.CurrentValue.position.x - transform.CurrentValue.position.x;

                facing.Value = direction switch
                {
                    < 0 when facing.CurrentValue != Facing.Left => Facing.Left,
                    > 0 when facing.CurrentValue != Facing.Right => Facing.Right,
                    _ => facing.CurrentValue
                };
            };
        }
    }
}