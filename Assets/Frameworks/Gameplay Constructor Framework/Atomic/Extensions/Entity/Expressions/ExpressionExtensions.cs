using System.Collections.Generic;
using GameplayConstructorFramework.Entity;

namespace AtomicFramework.AtomicStructures
{
    public static class ExpressionExtensions
    {
        public static void AppendBy(this IAtomicExpression<bool> expression, IEntityConditionFabric[] conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Length;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
        public static void AppendBy(this IAtomicExpression<bool> expression, IReadOnlyList<IEntityConditionFabric> conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Count;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
        
        public static void AppendBy(this IAtomicExpression<float> expression, IEntityFloatExpressionElementFabric[] conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Length;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
        
        public static void AppendBy(this IAtomicExpression<int, bool> expression, IEntityConditionFabric<int>[] conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Length;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
        public static void AppendBy(this IAtomicExpression<int, bool> expression, IReadOnlyList<IEntityConditionFabric<int>> conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Count;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
        
        public static void AppendBy(this IAtomicExpression<int, float> expression, IEntityFloatExpressionElementFabric<int>[] conditionBuilders, IEntity entity)
        {
            var count = conditionBuilders.Length;
            
            if(count == 0) return;

            for (var i = 0; i < count; i++)
            {
                expression.Append(conditionBuilders[i].Create(entity));
            }
        }
    }
}