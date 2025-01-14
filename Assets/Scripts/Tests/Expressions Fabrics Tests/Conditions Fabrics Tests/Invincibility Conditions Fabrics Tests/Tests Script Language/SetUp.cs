using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace ConditionsFabricsTests.InvincibilityConditionsFabricsTests.TestsScriptLanguage
{
    public static class SetUp
    {
        public static Func<bool> IsInvincibleConditionForEntityWithoutInvincibility()
        {
            var entity = Tests.EntityExtensionsTests.TestsScriptLanguage.Create.Entity();
            var isInvincibleConditionFabric = Create.IsInvincibleConditionFabric();
            var isInvincibleCondition = isInvincibleConditionFabric.CreateFor(entity);
            return isInvincibleCondition;
        }
        
        public static Func<bool> IsInvincibleConditionForEntityWithInvincibility(bool invincibility = false)
        {
            var entity = EntityWithInvincibility(invincibility);
            var isInvincibleConditionFabric = Create.IsInvincibleConditionFabric();
            var isInvincibleCondition = isInvincibleConditionFabric.CreateFor(entity);
            return isInvincibleCondition;
        }

        public static IEntity EntityWithInvincibility(bool invincibility)
        {
            var entity = Tests.EntityExtensionsTests.TestsScriptLanguage.Create.Entity();
            entity.TryAddInvincibilityData(new AtomicReactiveProperty<bool>(invincibility));
            return entity;
        }

        public static Func<bool> IsNotInvincibleConditionForEntityWithoutInvincibility()
        {
            var entity = Tests.EntityExtensionsTests.TestsScriptLanguage.Create.Entity();
            var isNotInvincibleConditionFabric = Create.IsNotInvincibleConditionFabric();
            var isNotInvincibleСondition = isNotInvincibleConditionFabric.CreateFor(entity);
            return isNotInvincibleСondition;
        }
        
        public static Func<bool> IsNotInvincibleConditionForEntityWithInvincibility(bool invincibility = false)
        {
            var entity = EntityWithInvincibility(invincibility);
            var isNotInvincibleConditionFabric = Create.IsNotInvincibleConditionFabric();
            var isNotInvincibleСondition = isNotInvincibleConditionFabric.CreateFor(entity);
            return isNotInvincibleСondition;
        }
    }
}