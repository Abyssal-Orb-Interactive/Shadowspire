using System;

namespace Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests.TestsScriptLanguage
{
    public static class SetUp
    {
        public static Func<bool> IsAliveConditionForEntityWithoutHealth()
        {
            var entity = EntityExtensionsTests.TestsScriptLanguage.Create.Entity();
            var conditionFabric = Create.IsAliveConditionFabric();
            var condition = conditionFabric.CreateFor(entity);
            return condition;
        }
        
        public static Func<bool> IsAliveConditionForEntityWithHealth(in float health = 0f)
        {
            var entity = EntityExtensionsTests.TestsScriptLanguage.SetUp.EntityWithHealth(health);
            var conditionFabric = Create.IsAliveConditionFabric();
            var condition = conditionFabric.CreateFor(entity);
            return condition;
        }

        public static Func<bool> IsDeadConditionForEntityWithoutHealth()
        {
            var entity = EntityExtensionsTests.TestsScriptLanguage.Create.Entity();
            var isDeadConditionFabric = Create.IsDeadConditionFabric();
            var isDeadCondition = isDeadConditionFabric.CreateFor(entity);
            return isDeadCondition;
        }
        
        public static Func<bool> IsDeadConditionForEntityWithHealth(float health = 0f)
        {
            var entity = EntityExtensionsTests.TestsScriptLanguage.SetUp.EntityWithHealth(health);
            var isDeadConditionFabric = Create.IsDeadConditionFabric();
            var isDeadCondition = isDeadConditionFabric.CreateFor(entity);
            return isDeadCondition;
        }
    }
}