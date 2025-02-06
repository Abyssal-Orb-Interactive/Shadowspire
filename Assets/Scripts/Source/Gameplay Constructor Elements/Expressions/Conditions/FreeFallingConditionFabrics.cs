using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Expressions.Conditions
{
    [Serializable]
    public sealed class IsFreeFallingConditionFabric : IEntityConditionFabric
    {
        [Obsolete("Obsolete")]
        public Func<bool> CreateFor(IEntity entity)
        {
            return () => entity.TryGetRigidbody2DData(out var rigidbody2D) && rigidbody2D.CurrentValue.velocity.y < 0f;
        }
    }
}