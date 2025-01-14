using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using UnityEngine;

namespace Tests.EntityExtensionsTests.TestsScriptLanguage
{
    public static class Create
    {
        public static IEntity Entity()
        {
            return new Entity();
        }

        public static GameObject GameObject()
        {
            return new GameObject();
        }

        public static Component DamagerComponent()
        {
            var damagerGameObject = GameObject();
            var damgerComponent = damagerGameObject.transform;
            return damgerComponent;
        }
        
        public static IEntityConditionFabric[] ConditionFabricsWithIsAliveConditionFabric()
        {
            return new IEntityConditionFabric[] { ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests.TestsScriptLanguage.Create.IsAliveConditionFabric() };
        }

        public static AtomicBoolMultiplication CanTakeDamageConditionByBoolMultiplication()
        {
            return new AtomicBoolMultiplication();
        }
    }
}