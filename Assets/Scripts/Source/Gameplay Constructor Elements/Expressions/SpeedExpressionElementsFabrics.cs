using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace Source.Gameplay_Constructor_Elements.Expressions
{
    [Serializable]
    public sealed class SpeedExpressionBaseSpeedElementFabric : IEntityFloatExpressionElementFabric
    {
        public Func<float> CreateFor(IEntity entity)
        {
            return () => !entity.TryGetSpeedData(out var speed) ? 0f : speed.CurrentValue;
        }
    }
}